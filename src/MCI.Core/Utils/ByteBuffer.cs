namespace Miharu.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Obsolete("Miharu.Utils.RingBuffer<byte> を利用してください。")]
    public class ByteBuffer
    {
        public ByteBuffer(int maxLength)
        {
            this.MaxLength = maxLength;
            this.Buffer = new byte[maxLength];
            this.CurrentLength = 0;
        }


        public ByteBuffer(byte[] buffer)
        {
            this.MaxLength = buffer.Length;
            this.Buffer = buffer;
            this.CurrentLength = buffer.Length;
        }


        public int MaxLength { get; private set; }
        public byte[] Buffer { get; private set; }
        public int CurrentLength { get; private set; }

        public byte[] Result
        {
            get
            {
                // 遅い？
                return this.Buffer.Take(this.CurrentLength).ToArray();
            }
        }


        public bool CanAdd(byte[] packet)
        {
            return this.CurrentLength + packet.Length <= this.MaxLength;
        }


        public bool CanAdd(int length)
        {
            return this.CurrentLength + length <= this.MaxLength;
        }

        public void Add(byte[] packet)
        {
            if (!this.CanAdd(packet))
            {
                // エラー
                throw new ArgumentException("Buffer に入りきらない量のデータです。");
            }

            for (var i = 0; i < packet.Length; i++)
            {
                this.Buffer[i + this.CurrentLength] = packet[i];
            }

            this.CurrentLength += packet.Length;
        }


        public void Add(byte[] packet, int length)
        {
            if (!this.CanAdd(length))
            {
                // エラー
                throw new ArgumentException("Buffer に入りきらない量のデータです。");
            }

            for (var i = 0; i < length; i++)
            {
                this.Buffer[i + this.CurrentLength] = packet[i];
            }

            this.CurrentLength += length;
        }


        public void Add(byte[] packet, int offset, int length)
        {
            if (packet.Length < offset + length)
            {
                // エラー
                throw new ArgumentOutOfRangeException("offset, length の長さが不正です。");
            }

            if (!this.CanAdd(length))
            {
                // エラー
                throw new ArgumentException("Buffer に入りきらない量のデータです。");
            }

            for (var i = 0; i < length; i++)
            {
                this.Buffer[i + this.CurrentLength] = packet[offset + i];
            }

            this.CurrentLength += length;
        }


        public byte[] Take(int length)
        {
            if (this.CurrentLength < length)
            {
                throw new ArgumentOutOfRangeException("length の長さが不正です。");
            }

            var result = this.Buffer.Take(length).ToArray();

            this.CurrentLength -= length;
            for (var i = 0; i < this.CurrentLength; i++)
            {
                this.Buffer[i] = this.Buffer[i + length];
            }

            return result;
        }

        public void Skip(int length)
        {
            if (this.CurrentLength < length)
            {
                this.CurrentLength = 0;
                return;
            }

            this.CurrentLength -= length;
            for (var i = 0; i < this.CurrentLength; i++)
            {
                this.Buffer[i] = this.Buffer[i + length];
            }
        }

        public void Clear()
        {
            this.CurrentLength = 0;
        }
    }
}
