using NServiceBus;
using NServiceBus.Logging;

public class AuditThisMessageHandler :
    IHandleMessages<AuditThisMessage>
{
    static ILog log = LogManager.GetLogger<AuditThisMessageHandler>();

    public void Handle(AuditThisMessage message)
    {
        log.Info($"Handling {message.GetType().Name}");
    }
}