using System.IO;

namespace PokemonGenerator.Tests.Integration.IO_Tests
{
    class StreamShim : Stream
    {
        private Stream _realStram;

        public StreamShim(Stream realStram) : base()
        {
            _realStram = realStram;
        }

        protected override void Dispose(bool disposing)
        {
            /* Do Nothing */
        }

        public override void Close()
        {
            /* Do Nothing */
        }

        public override void Flush() => _realStram.Flush();

        public override long Seek(long offset, SeekOrigin origin) => _realStram.Seek(offset, origin);

        public override void SetLength(long value) => _realStram.SetLength(value);

        public override int Read(byte[] buffer, int offset, int count) => _realStram.Read(buffer, offset, count);

        public override void Write(byte[] buffer, int offset, int count) => _realStram.Write(buffer, offset, count);

        public override bool CanRead => _realStram.CanRead;
        public override bool CanSeek => _realStram.CanSeek;
        public override bool CanWrite => _realStram.CanWrite;
        public override long Length => _realStram.Length;

        public override long Position
        {
            get => _realStram.Position;
            set => _realStram.Position = value;
        }
    }
}
