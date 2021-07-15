using System;


public interface IScoreSendable
{
    event Action<int> OnChangeScore;
}
