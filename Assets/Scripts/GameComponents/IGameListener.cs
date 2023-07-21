using System;

namespace GameComponents
{
    public interface IGameListener : IGameStartListener, 
                                        IGamePauseListener, 
                                        IGameResumeListener, 
                                        IGameEndListener,
                                        IGameDisposable{ }
    
    public interface IGameStartListener
    {
        void OnGameStarted();
    }

    public interface IGamePauseListener
    {
        void OnGamePaused();
    }

    public interface IGameResumeListener
    {
        void OnGameResumed();
    }

    public interface IGameEndListener
    {
        void OnGameEnded();
    }

    public interface IGameDisposable
    {
        void OnGameDispose();
    }
    
}