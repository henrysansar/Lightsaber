using Sansar.Simulation;
using System;
using Sansar.Script;
using Sansar;
public class LightSaber : SceneObjectScript
{
	private RigidBodyComponent RigidBody = null;
    private AnimationComponent Animation = null;
    public SoundResource SoundLightOn;
    public SoundResource SoundIdle;
    public SoundResource SoundClash;
    public bool IdleSound = true;
    public bool ReactToCollisions = true;
    private AudioComponent SpeakerOnOff = null;
    private PlayHandle ph;
    private LightComponent Light = null;
    public Color ReflexColor;
    private bool isOn = false;
    private AgentPrivate me = null;
	public override void Init()
	{
		ObjectPrivate.TryGetFirstComponent<RigidBodyComponent>(out RigidBody);
        ObjectPrivate.TryGetFirstComponent<AnimationComponent>(out Animation);
        ObjectPrivate.TryGetFirstComponent<AudioComponent>(out SpeakerOnOff);
        ObjectPrivate.TryGetFirstComponent<LightComponent>(out Light);
        resetSaber();
		RigidBody.SubscribeToHeldObject(HeldObjectEventType.Grab, (HeldObjectData heldData) =>
		{
			AgentPrivate me = ScenePrivate.FindAgent(heldData.HeldObjectInfo.SessionId);
            Wait(TimeSpan.FromSeconds(1.0f));
            Animation an = Animation.DefaultAnimation;
            an.JumpToFrame(0);
            AnimationParameters ap = new AnimationParameters();
             ap.PlaybackMode = AnimationPlaybackMode.PlayOnce;
             ap.PlaybackSpeed = 0.9f;
             WaitFor(an.Reset, ap);
             an.Play();            
             SpeakerOnOff.PlaySoundOnComponent(SoundLightOn, PlaySettings.PlayOnce); 
             Light.SetColorAndIntensity(ReflexColor, 3.0f);
             Wait(TimeSpan.FromSeconds(1.0f));
             if(IdleSound) {
                ph = SpeakerOnOff.PlaySoundOnComponent(SoundIdle, PlaySettings.Looped);    
             }
             isOn = true;
            if(ReactToCollisions) {
                CheckForCollisions(CollisionEventType.AllCollisions);
            }
		});
        RigidBody.SubscribeToHeldObject(HeldObjectEventType.Release, (HeldObjectData heldData) =>
		{
            isOn = false;
            Light.SetColorAndIntensity(ReflexColor, 0.0f);
            if(IdleSound) {
                ph.Stop();
            }
            resetSaber();
		});
	}
    private void resetSaber() {
        Light.SetColorAndIntensity(ReflexColor, 0.0f);
        Animation an = Animation.DefaultAnimation;
        AnimationParameters ap = new AnimationParameters();
        an.JumpToFrame(0);
    }
    private void CheckForCollisions(CollisionEventType trackedEvents) {
        if(!isOn) {
            return;
        }
        CollisionData data = (CollisionData) WaitFor(RigidBody.Subscribe, trackedEvents, Sansar.Script.ComponentId.Invalid);
        SpeakerOnOff.PlaySoundOnComponent(SoundClash, PlaySettings.PlayOnce); 
        Light.SetColorAndIntensity(ReflexColor, 15.0f);
        Wait(TimeSpan.FromSeconds(0.3f));
        if(isOn) {
            Light.SetColorAndIntensity(ReflexColor, 3.0f);    
        } else {
            Light.SetColorAndIntensity(ReflexColor, 0.0f);
        }        
        CheckForCollisions(CollisionEventType.AllCollisions);
	}
}