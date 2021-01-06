const { app, BrowserWindow } = require('electron')
const path = require('path')
const { ipcMain } = require('electron');
const { powerSaveBlocker } = require('electron')

powerSaveBlocker.start('prevent-app-suspension');
powerSaveBlocker.start('prevent-display-sleep')

app.allowRendererProcessReuse=true;

// 主要窗口的对象
var mainWindow;

function createWindow() {
  mainWindow = new BrowserWindow({
    width: 1280,
    height: 720,
    useContentSize: true,
    resizable: false,
    autoHideMenuBar: true,
    // alwaysOnTop:true,
    webPreferences: {
      backgroundThrottling: false,
      pageVisibility: true,
      preload: path.join(__dirname, 'preload.js'),
      nodeIntegration: true
    }
  })

  mainWindow.loadURL('file://' + __dirname + '/index.html');

  mainWindow.on('closed', function () {
    mainWindow = null
  })
}
app.commandLine.appendSwitch('autoplay-policy', 'no-user-gesture-required');
app.commandLine.appendSwitch('disable-renderer-backgrounding');

app.on('ready', createWindow)

app.on('window-all-closed', function () {
  app.quit()
})

app.on('activate', function () {
  if (mainWindow === null) {
    createWindow()
  }
})

var floating = false;
var oldSize;



ipcMain.on('resize', function (e, x, y) {
  oldSize = [x, y];
  mainWindow.setContentSize(x, y);
})

ipcMain.on('float', function () {
  if (!floating) {
    mainWindow.setAlwaysOnTop(true);
    mainWindow.setVisibleOnAllWorkspaces(true);
    mainWindow.setPosition(0, 0);
    mainWindow.setContentSize(1, 1);
  } else {

    mainWindow.setAlwaysOnTop(false);
    mainWindow.setVisibleOnAllWorkspaces(false);
    mainWindow.setContentSize(oldSize[0], oldSize[1]);
  }
  floating = !floating;
})

