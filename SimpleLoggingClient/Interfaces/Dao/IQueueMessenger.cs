using System.Threading.Tasks;

namespace SimpleLoggingClient.LoggingInterfaces.Dao
{
    public interface IQueueMessenger
    {
        Task<bool> SendMessage(byte[] message);
    }
}