using System;

namespace MyUtility
{
    public interface IImpactLogic
    {
        void StartImpact(CharacterInfo obj, int impactId);
        void Tick(CharacterInfo obj, int impactId);
        void StopImpact(CharacterInfo obj, int impactId);
        void OnInterrupted(CharacterInfo obj, int impactId);
        void OnAddImpact(CharacterInfo obj, int impactId, int addImpactId);
    }
}
