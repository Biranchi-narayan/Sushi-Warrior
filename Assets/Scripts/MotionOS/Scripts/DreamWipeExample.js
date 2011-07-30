var camera1 : Camera;
var camera2 : Camera;
var fadeTime = 4.0;
var waveScale = .07;				// Higher numbers make the effect more exaggerated. Can be negative, .5/-.5 is the max
var waveFrequency = 25.0;			// Higher numbers make more waves in the effect
private var inProgress = false;
private var swap = false;

private var pending = false;

function Start () {
	ScreenWipe.use.InitializeDreamWipe();
}

function Update () {
	if (Input.GetKeyDown("space")) {
		DoFade();
	}
	if ((!inProgress) && (pending)) {
		pending = false;
		DoFade();
	}
}

function DoFade () {
	if (inProgress) {
		pending = true;
		return;
	}
	inProgress = true;
	
	swap = !swap;
	//yield ScreenWipe.use.DreamWipe (swap? camera1 : camera2, swap? camera2 : camera1, fadeTime, waveScale, waveFrequency);
	camera1.gameObject.SetActiveRecursively(!swap);
	camera2.gameObject.SetActiveRecursively(swap);
	inProgress = false;
}