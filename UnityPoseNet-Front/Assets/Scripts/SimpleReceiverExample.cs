using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using LitJson;

public class SimpleReceiverExample : MonoBehaviour
{

    public PointPlacer placer;

    private OSCReciever reciever;

    public int port = 9527;

    // Use this for initialization
    void Start()
    {
        reciever = new OSCReciever();
        reciever.Open(port);
        //
    }

    private void Awake()
    {
        placer = FindObjectOfType<PointPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reciever.hasWaitingMessages())
        {
            OSCMessage msg = reciever.getNextMessage();
            Debug.Log(string.Format("message received: {0} {1}", msg.Address, DataToString(msg.Data)));
            //Debug.Log(msg.Data[0]);
            JsonData data = JsonMapper.ToObject(msg.Data[0].ToString());
            //Debug.Log(data["keypoints"][0]["x"]);
            int width = (int)data["width"];
            int height = (int)data["height"];
            //Debug.Log(string.Format("{0}  {1}",width,height));
            placer.body.width = width;
            placer.body.height = height;

            /*
	 
		 
	{"keypoints":
	[{"score":0.9994081258773804,"part":"nose","position":{"x":320.14340649319064,"y":303.17838794990274}},
	{"score":0.9996917247772217,"part":"leftEye","position":{"x":384.09175887645915,"y":262.758922057393}},
	{"score":0.9996007084846497,"part":"rightEye","position":{"x":283.9241473127432,"y":256.98869163424126}},
	{"score":0.9866662621498108,"part":"leftEar","position":{"x":459.4115545963035,"y":284.54530946011675}},
	{"score":0.8088226318359375,"part":"rightEar","position":{"x":242.28523224708172,"y":267.856844297179}},
	{"score":0.6614661812782288,"part":"leftShoulder","position":{"x":593.2875577577821,"y":496.19790400048635}},
	{"score":0.9601513147354126,"part":"rightShoulder","position":{"x":152.65926138436285,"y":481.06103325632296}},
	{"score":0.028393439948558807,"part":"leftElbow","position":{"x":620.7999072835603,"y":465.71828641780155}},
	{"score":0.015384284779429436,"part":"rightElbow","position":{"x":103.54781926374028,"y":568.8118616245137}},
	{"score":0.020619602873921394,"part":"leftWrist","position":{"x":567.5149714250973,"y":466.77570981274323}},
	{"score":0.00730806915089488,"part":"rightWrist","position":{"x":276.8106874696012,"y":512.6236092534047}},
	{"score":0.012575170956552029,"part":"leftHip","position":{"x":466.64320890077823,"y":474.83717625243196}},
	{"score":0.012927847914397717,"part":"rightHip","position":{"x":242.79481167923151,"y":557.0765442607004}},
	{"score":0.00571694178506732,"part":"leftKnee","position":{"x":581.5637159533075,"y":465.0811268847276}},
	{"score":0.0038781538605690002,"part":"rightKnee","position":{"x":154.19249300826849,"y":493.0039746473735}},
	{"score":0.010026947595179081,"part":"leftAnkle","position":{"x":548.4713566999027,"y":468.20779882052534}},
	{"score":0.005594655405730009,"part":"rightAnkle","position":{"x":549.8559475316148,"y":467.963468202821}}],
	"width":640,"height":480}	 
		 
	*/
            placer.body.nose.SetValue(width - float.Parse( data["keypoints"][0]["x"].ToString()), height- float.Parse(data["keypoints"][0]["y"].ToString()));
            placer.body.leftEye.SetValue(width - float.Parse(data["keypoints"][1]["x"].ToString()), height - float.Parse(data["keypoints"][1]["y"].ToString()));
            placer.body.rightEye.SetValue(width - float.Parse(data["keypoints"][2]["x"].ToString()), height - float.Parse(data["keypoints"][2]["y"].ToString()));
            placer.body.leftEar.SetValue(width - float.Parse(data["keypoints"][3]["x"].ToString()), height - float.Parse(data["keypoints"][3]["y"].ToString()));
            placer.body.rightEar.SetValue(width - float.Parse(data["keypoints"][4]["x"].ToString()), height - float.Parse(data["keypoints"][4]["y"].ToString()));
            placer.body.leftShoulder.SetValue(width - float.Parse(data["keypoints"][5]["x"].ToString()), height - float.Parse(data["keypoints"][5]["y"].ToString()));
            placer.body.rightShoulder.SetValue(width - float.Parse(data["keypoints"][6]["x"].ToString()), height - float.Parse(data["keypoints"][6]["y"].ToString()));
            placer.body.leftElbow.SetValue(width - float.Parse(data["keypoints"][7]["x"].ToString()), height - float.Parse(data["keypoints"][7]["y"].ToString()));
            placer.body.rightElbow.SetValue(width - float.Parse(data["keypoints"][8]["x"].ToString()), height - float.Parse(data["keypoints"][8]["y"].ToString()));
            placer.body.leftWrist.SetValue(width - float.Parse(data["keypoints"][9]["x"].ToString()), height - float.Parse(data["keypoints"][9]["y"].ToString()));
            placer.body.rightWrist.SetValue(width - float.Parse(data["keypoints"][10]["x"].ToString()), height - float.Parse(data["keypoints"][10]["y"].ToString()));
            placer.body.leftHip.SetValue(width - float.Parse(data["keypoints"][11]["x"].ToString()), height - float.Parse(data["keypoints"][11]["y"].ToString()));
            placer.body.rightHip.SetValue(width - float.Parse(data["keypoints"][12]["x"].ToString()), height - float.Parse(data["keypoints"][12]["y"].ToString()));
            placer.body.leftKnee.SetValue(width - float.Parse(data["keypoints"][13]["x"].ToString()), height - float.Parse(data["keypoints"][13]["y"].ToString()));
            placer.body.rightKnee.SetValue(width - float.Parse(data["keypoints"][14]["x"].ToString()), height - float.Parse(data["keypoints"][14]["y"].ToString()));
            placer.body.leftAnkle.SetValue(width - float.Parse(data["keypoints"][15]["x"].ToString()), height - float.Parse(data["keypoints"][15]["y"].ToString()));
            placer.body.rightAnkle.SetValue(width - float.Parse(data["keypoints"][16]["x"].ToString()), height - float.Parse(data["keypoints"][16]["y"].ToString()));

            placer.SetPosition();
        }
    }



    private string DataToString(List<object> data)
    {
        string buffer = "";

        for (int i = 0; i < data.Count; i++)
        {
            buffer += data[i].ToString() + " ";
        }

        buffer += "\n";

        return buffer;
    }
}
