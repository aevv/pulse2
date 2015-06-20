// MenuScreen initialisation
// Create UI elements
var background = factories['background'].Create("Assets\\bg.jpg");

var bFac = factories['button'];

var width = _pulse.Config.Width;
var height = _pulse.Config.Height;

var playBtn = bFac.Create(width / 2 - 100, height / 2 + 100, 0, 200, 50, "Play");
var optionsBtn = bFac.Create(width / 2 - 100, height / 2 + height / 10 + 100, 0, 200, 50, "Options");
var exitBtn = bFac.Create(width / 2 - 100, height / 2 + (height / 10 * 2) + 100, 0, 200, 50, "Exit");
var anim = factories['animation'].Create(200, 200, 10, 300, 300, 'Assets/Burst/Burst{0}.png', 7);
anim.Loop = true;

// Click events
playBtn.OnClick = function() {
    _pulse.ScreenManager.setActive("GameScreen");
};

optionsBtn.OnClick = function() {
    log("Test tracesource functions or w/e");
};

exitBtn.OnClick = function() {
    _pulse.Game.Quit();
};

// Register controls on the screen
currentScreen.RegisterControl(background);
currentScreen.RegisterControl(playBtn);
currentScreen.RegisterControl(optionsBtn);
currentScreen.RegisterControl(exitBtn);
currentScreen.RegisterControl(anim);

// Play a song
_pulse.Media.PlayRandom();