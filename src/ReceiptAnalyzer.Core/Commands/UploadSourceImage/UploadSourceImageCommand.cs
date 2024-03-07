using MediatR;

namespace BS.ReceiptAnalyzer.Core.Commands.UploadSourceImage
{
    public sealed record UploadSourceImageCommand(Guid TaskId, string MIME, byte[] Image)
        : CommandBase, IRequest<UploadSourceImageCommandResult>;
}
