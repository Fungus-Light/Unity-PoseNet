const fs = require('fs');
const { ipcRenderer } = require('electron');
const tf = require('@tensorflow/tfjs')
const posenet = require('@tensorflow-models/posenet');
const OSC = require('osc-js');

var osc;

var camW;
var camH;

const Stats = require('stats.js');
var stats = new Stats();
stats.showPanel(0);
document.body.appendChild(stats.dom);

var settings = JSON.parse(fs.readFileSync(__dirname + "/settings.json", "utf8"));

function openOSC() {
  osc = new OSC({
    plugin: new OSC.DatagramPlugin({
      send: {
        host: settings.host,
        port: settings.port,
      }
    })
  });
  osc.open();
}

openOSC()



var camera = document.getElementById('camera');
var inputCanvas = document.createElement("canvas");
var debugCanvas = document.createElement("canvas");
var messageDiv = document.createElement("div");

document.body.appendChild(debugCanvas);

camera.style.position = "absolute";
camera.style.left = "0px";
camera.style.top = "0px";

debugCanvas.style.position = "absolute";
debugCanvas.style.left = "0px";
debugCanvas.style.top = "0px";

messageDiv.style.width = "100%";
messageDiv.style.position = "absolute";
messageDiv.style.left = "0px";
messageDiv.style.bottom = "0px";
messageDiv.style.backgroundColor = "rgba(0,0,0,0.4)";
messageDiv.style.color = "white";
messageDiv.style.fontFamily = "monospace"
document.body.appendChild(messageDiv);


var audio = document.createElement("audio");
audio.controls = "controls";
audio.loop = "loop";
audio.autoplay = "autoplay";
audio.volume = "0.001";
var source = document.createElement("source");
source.src = "https://www.w3schools.com/html/horse.mp3";
audio.appendChild(source);
audio.style.position = "absolute";
audio.style.left = "200px";
audio.style.top = "0px";
audio.style.display = "none";
document.body.appendChild(audio);

var net = undefined;

var testImage = undefined;
var testImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4d/Rembrandt_-_The_Anatomy_Lesson_of_Dr_Nicolaes_Tulp.jpg/637px-Rembrandt_-_The_Anatomy_Lesson_of_Dr_Nicolaes_Tulp.jpg";
var frameCount = 0;

navigator.mediaDevices.getUserMedia({ video: true })
  .then(function (stream) {
    camera.srcObject = stream;
  }).catch(function () {
    alert('no video stream');
  });

function generateGUI() {
  var div = document.createElement("div");
  div.style.color = "white";
  div.style.fontFamily = "monospace";
  var d = document.createElement("div");
  d.innerHTML = "settings";
  d.style.backgroundColor = "rgba(0,0,0,0.3)"
  div.appendChild(d);

  for (var k in settings) {
    var d = document.createElement("div")
    var lbl = document.createElement("span");
    lbl.innerHTML = k;

    if (typeof (settings[k]) == 'boolean') {
      var cb = document.createElement("input");
      cb.type = "checkbox";
      cb.checked = settings[k];

      ; (function () {
        var _k = k;
        var _cb = cb;
        _cb.onclick = function () {
          settings[_k] = _cb.checked;
        }
      })();
      d.appendChild(cb);
      d.appendChild(lbl);
    } else if (typeof (settings[k]) == 'string') {
      var inp = document.createElement("input");
      inp.value = settings[k];
      inp.style.backgroundColor = "rgba(0,0,0,0.3)";
      inp.style.color = "white";
      inp.style.fontFamily = "monospace";
      inp.style.border = "1px solid black";

      ; (function () {
        var _k = k;
        var _inp = inp;
        _inp.onkeypress = function () {
          if (event.key == "Enter") {
            settings[_k] = _inp.value;
          }
        }
      })();
      d.appendChild(lbl);
      d.appendChild(inp);
    }

    d.style.borderBottom = "1px solid black";
    div.appendChild(d);
  }
  document.body.appendChild(div);
  div.style.position = "absolute";
  div.style.left = "0px";
  div.style.top = "50px";
  div.style.backgroundColor = "rgba(0,0,0,0.5)"
}

generateGUI();

function drawPose(pose, color = "white") {
  var ctx = debugCanvas.getContext('2d');
  for (var i = 0; i < pose.keypoints.length; i++) {
    var p = pose.keypoints[i].position
    ctx.fillStyle = color;
    ctx.fillRect(p.x - 5, p.y - 5, 10, 10);
    ctx.fillStyle = "yellow";
    ctx.fillText(("0" + i).substr(-2) + " " + pose.keypoints[i].part, p.x + 5, p.y - 5);
  }
  const adj = [
    [0, 1], [0, 2], [1, 3], [2, 4],        //face
    [5, 6], [11, 12], [5, 11], [6, 12],    //body
    [5, 7], [7, 9], [6, 8], [8, 10],       //arms
    [11, 13], [13, 15], [12, 14], [14, 16],//legs
  ]
  const minConf = 0.5
  adj.forEach(([i, j]) => {
    if (pose.keypoints[i] < minConf || pose.keypoints[j] < minConf) {
      return;
    }
    ctx.beginPath();
    ctx.moveTo(pose.keypoints[i].position.x, pose.keypoints[i].position.y);
    ctx.lineTo(pose.keypoints[j].position.x, pose.keypoints[j].position.y);
    ctx.lineWidth = 2;
    ctx.strokeStyle = color;
    ctx.stroke();
  });
}

function sendPosesJSON(poses) {
  let keypoints = poses[0]["keypoints"];
  let positions = [];
  for (let i = 0; i < keypoints.length; i++) {
    positions.push(keypoints[i]["position"]);
  }
  //console.log(keypoints);
  let Tosend = {
    keypoints: positions,
    width: camW,
    height: camH
  };
  osc.send(new OSC.Message("/", JSON.stringify(Tosend)));
}


async function estimateFrame() {
  stats.begin();
  if (osc.options.plugin.options.send.host != settings.host
    || osc.options.plugin.options.send.port != settings.port) {
    openOSC();
  }

  var ictx = inputCanvas.getContext('2d');
  var dctx = debugCanvas.getContext('2d');

  var testImageXPos = undefined;
  if (settings.useTestImage) {
    if (!testImage) {
      testImage = new Image;
      testImage.src = testImageUrl;
    }
    if (testImage.complete) {
      testImageXPos = Math.sin(frameCount * 0.1) * 20;
      ictx.drawImage(testImage, testImageXPos, 0);
    }
  } else {
    ictx.drawImage(camera, 0, 0);

  }
  var poses = [];
  if (settings.multiplePoses) {
    poses = await net.estimateMultiplePoses(inputCanvas, {
      flipHorizontal: false
    });
  } else {
    poses[0] = await net.estimateSinglePose(inputCanvas, {
      flipHorizontal: false
    });
  }

  sendPosesJSON(poses);

  messageDiv.innerHTML = ["/", "-", "\\", "|"][frameCount % 4] + " number of person :" + poses.length + " , sending to : "
    + osc.options.plugin.options.send.host + ":"
    + osc.options.plugin.options.send.port;

  dctx.clearRect(0, 0, dctx.canvas.width, dctx.canvas.height);
  if (settings.useTestImage && testImageXPos != undefined) {
    dctx.drawImage(testImage, testImageXPos, 0);
  }
  poses.forEach((pose, i) => {
    drawPose(pose, ["white", "cyan", "magenta", "yellow"][i % 5]);
  })

  stats.end();


  frameCount++;
}

messageDiv.innerHTML = "initiate app"
camera.onloadeddata = function () {
  messageDiv.innerHTML = "camera loaded, now loading tf model"
  var [w, h] = [camera.videoWidth, camera.videoHeight];

  console.log(w, h);
  camW = w;
  camH = h;
  inputCanvas.width = w;
  inputCanvas.height = h;

  debugCanvas.width = w;
  debugCanvas.height = h;

  ipcRenderer.send('resize', w, h);

  posenet.load(settings.poseNetConfig).then(function (_net) {
    messageDiv.innerHTML = "everything loaded! Press X to float the window!"
    net = _net;
    setInterval(estimateFrame, 5);
  });
}
//浮动窗口
document.body.addEventListener("keypress", function (event) {
  if (event.key == 'x') {
    ipcRenderer.send('float');
  }
})
