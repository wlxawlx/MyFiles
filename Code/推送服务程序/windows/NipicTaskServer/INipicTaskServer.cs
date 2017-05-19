using System;

namespace NipicTask
{

    public interface INipicTaskServer
    {

        void Initialize();

        void Start();

        void Stop();

        void Pause();

        void Resume();
    }
}