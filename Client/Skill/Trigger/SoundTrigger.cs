using System;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;

namespace Client.SkillTrigger
{
    public class AudioManager
    {
        public bool IsContainAudioSource(string name)
        {
            return m_AudioDict.ContainsKey(name);
        }

        public AudioSource GetAudioSource(string name)
        {
            AudioSource result = null;
            m_AudioDict.TryGetValue(name, out result);
            return result;
        }

        public void AddAudioSource(string name, AudioSource source)
        {
            if (!m_AudioDict.ContainsKey(name))
            {
                m_AudioDict.Add(name, source);
            }
        }

        private Dictionary<string, AudioSource> m_AudioDict = new Dictionary<string, AudioSource>();
    }

    public class PlaySoundTriger : AbstractSkillTriger
    {
        public override ISkillTriger Clone()
        {
            PlaySoundTriger triger = new PlaySoundTriger();
            triger.m_StartTime = m_StartTime;
            triger.m_Name = m_Name;
            triger.m_AudioSource = m_AudioSource;
            triger.m_AudioSourceLifeTime = m_AudioSourceLifeTime;
            triger.m_AudioGroup.AddRange(m_AudioGroup);
            triger.m_IsNeedCollide = m_IsNeedCollide;
            triger.m_IsBoneSound = m_IsBoneSound;
            triger.m_Position = m_Position;
            triger.m_BoneName = m_BoneName;
            triger.m_IsAttach = m_IsAttach;
            return triger;
        }
        public override bool Execute(object sender, SkillInstance instance, long delta, long curSectionTime)
        {
            GameObject obj = sender as GameObject;
            if (null == obj)
            {
                return false;
            }
            if (curSectionTime < m_StartTime)
            {
                return true;
            }
            if (m_IsNeedCollide)
            {
                SkillDamageManager damage_manager = instance.CustomDatas.GetData<SkillDamageManager>();
                if (damage_manager == null || !damage_manager.IsDamagedEnemy)
                {
                    return false;
                }
            }
            AudioManager audio_mgr = instance.CustomDatas.GetData<AudioManager>();
            if (audio_mgr == null)
            {
                audio_mgr = new AudioManager();
                instance.CustomDatas.AddData<AudioManager>(audio_mgr);
                audio_mgr.AddAudioSource(DefaultAudioName, obj.GetComponent<AudioSource>());
            }
            string random_audio = GetRandomAudio();
            AudioClip clip = ResourceSystem.GetSharedResource(random_audio) as AudioClip;
            if (null == clip)
            {
                return false;
            }
            AudioSource audiosource = audio_mgr.GetAudioSource(m_Name);
            if (audiosource == null)
            {
                audiosource = CreateNewAudioSource(obj);
                if (audiosource != null)
                {
                    audio_mgr.AddAudioSource(m_Name, audiosource);
                }
                else
                {
                    audiosource = obj.GetComponent<AudioSource>();
                }
            }
            if (audiosource != null)
            {
                if (audiosource.loop)
                {
                    audiosource.clip = clip;
                    audiosource.Play();
                }
                else
                {
                    audiosource.PlayOneShot(clip);
                }
            }
            return false;
        }

        private string GetRandomAudio()
        {
            int random_index = m_Random.Next(0, m_AudioGroup.Count);
            if (0 <= random_index && random_index < m_AudioGroup.Count)
            {
                return m_AudioGroup[random_index];
            }
            return "";
        }

        private AudioSource CreateNewAudioSource(GameObject obj)
        {
            if (string.IsNullOrEmpty(m_AudioSource))
            {
                return null;
            }
            GameObject audiosource_obj = ResourceSystem.NewObject(
                                                   m_AudioSource,
                                                   m_AudioSourceLifeTime / 1000.0f) as GameObject;
            if (audiosource_obj == null)
            {
                return null;
            }
            AudioSource audiosource = audiosource_obj.GetComponent<AudioSource>();
            if (m_IsBoneSound)
            {
                Transform attach_node = TriggerUtil.GetChildNodeByName(obj, m_BoneName);
                if (attach_node != null)
                {
                    audiosource.transform.parent = attach_node;
                    audiosource.transform.rotation = Quaternion.identity;
                    audiosource.transform.position = Vector3.zero;
                    if (!m_IsAttach)
                    {
                        audiosource.transform.parent = null;
                    }
                }
                else
                {
                    audiosource.transform.position = obj.transform.TransformPoint(m_Position);
                    if (m_IsAttach)
                    {
                        audiosource.transform.parent = obj.transform;
                    }
                }
            }
            else
            {
                audiosource.transform.position = obj.transform.TransformPoint(m_Position);
                if (m_IsAttach)
                {
                    audiosource.transform.parent = obj.transform;
                }
            }
            return audiosource;
        }

        protected override void Load(CallData callData)
        {
            int num = callData.GetParamNum();
            if (num >= 6)
            {
                m_StartTime = long.Parse(callData.GetParamId(0));
                m_Name = callData.GetParamId(1);
                m_AudioSource = callData.GetParamId(2);
                m_AudioSourceLifeTime = long.Parse(callData.GetParamId(3));
                m_AudioGroup.Add(callData.GetParamId(4));
                m_IsNeedCollide = bool.Parse(callData.GetParamId(5));
            }
        }

        protected override void Load(FunctionData funcData)
        {
            CallData callData = funcData.Call;
            if (null == callData)
            {
                return;
            }
            Load(callData);
            foreach (ISyntaxComponent statement in funcData.Statements)
            {
                CallData stCall = statement as CallData;
                if (null == stCall)
                {
                    continue;
                }
                if (stCall.GetId() == "position")
                {
                    LoadPositionConfig(stCall);
                }
                else if (stCall.GetId() == "bone")
                {
                    LoadBoneConfig(stCall);
                }
                else if (stCall.GetId() == "audiogroup")
                {
                    LoadAudioGroup(stCall);
                }
            }
        }

        private void LoadAudioGroup(CallData stCall)
        {
            for (int i = 0; i < stCall.GetParamNum(); i++)
            {
                m_AudioGroup.Add(stCall.GetParamId(i));
            }
        }

        private void LoadPositionConfig(CallData stCall)
        {
            if (stCall.GetParamNum() >= 2)
            {
                m_IsBoneSound = false;
                m_Position = ScriptableDataUtility.CalcVector3(stCall.GetParam(0) as CallData);
                m_IsAttach = bool.Parse(stCall.GetParamId(1));
            }
        }

        private void LoadBoneConfig(CallData stCall)
        {
            if (stCall.GetParamNum() >= 2)
            {
                m_IsBoneSound = true;
                m_Position = ScriptableDataUtility.CalcVector3(stCall.GetParam(0) as CallData); ;
                m_IsAttach = bool.Parse(stCall.GetParamId(1));
            }
        }

        public static string DefaultAudioName = "default";

        private string m_Name;
        private string m_AudioSource;
        private long m_AudioSourceLifeTime;
        private List<string> m_AudioGroup = new List<string>();
        private bool m_IsNeedCollide = false;

        private System.Random m_Random = new System.Random();
        private bool m_IsBoneSound = false;
        private Vector3 m_Position = new Vector3(0, 0, 0);
        private string m_BoneName = "";
        private bool m_IsAttach = true;
    }

    public class StopSoundTrigger : AbstractSkillTriger
    {
        public override ISkillTriger Clone()
        {
            StopSoundTrigger copy = new StopSoundTrigger();
            copy.m_StartTime = m_StartTime;
            copy.m_Name = m_Name;
            return copy;
        }

        public override void Reset()
        {
        }

        protected override void Load(CallData callData)
        {
            if (callData.GetParamNum() >= 2)
            {
                m_StartTime = long.Parse(callData.GetParamId(0));
                m_Name = callData.GetParamId(1);
            }
        }

        public override bool Execute(object sender, SkillInstance instance, long delta, long curSectionTime)
        {
            if (curSectionTime < m_StartTime)
            {
                return true;
            }
            GameObject obj = sender as GameObject;
            if (obj == null || obj.GetComponent<AudioSource>() == null)
            {
                return false;
            }
            AudioManager mgr = instance.CustomDatas.GetData<AudioManager>();
            if (mgr == null)
            {
                return false;
            }
            AudioSource source = mgr.GetAudioSource(m_Name);
            if (source != null)
            {
                source.Stop();
            }
            return false;
        }

        private string m_Name;
    }
}
