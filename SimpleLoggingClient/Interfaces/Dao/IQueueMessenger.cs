namespace SimpleLoggingClient.LoggingInterfaces.Dao
{
    public interface IQueueMessenger
    {
        void SendMessage(byte[] message);
    }
}