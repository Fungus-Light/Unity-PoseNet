using System;
using System.Collections.Generic;
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

[Serializable]
public class Body
{
	public int width;
	public int height;

	public Joint nose, leftEye, rightEye, leftEar, 
				rightEar, leftShoulder, rightShoulder, leftElbow, 
				rightElbow, leftWrist, rightWrist, leftHip, 
				rightHip, leftKnee, rightKnee, leftAnkle, rightAnkle;

}

[Serializable]
public class Joint
{
    public float x;
    public float y;
	public void SetValue(float _x,float _y)
	{
		x = _x;
		y = _y;
	}
}