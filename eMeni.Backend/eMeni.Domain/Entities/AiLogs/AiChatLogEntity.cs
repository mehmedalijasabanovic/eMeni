
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class AiChatLogEntity
{
    public int Id { get; set; }
    public int? UserId { get; set; }

    public Guid? SessionId { get; set; }

    public string Message { get; set; }

    public string Response { get; set; }

    public DateTime? Timestamp { get; set; }

    public string MessageType { get; set; }

    public eMeniUserEntity User { get; set; }
}