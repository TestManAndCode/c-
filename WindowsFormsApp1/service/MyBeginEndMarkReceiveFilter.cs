using SuperSocket.ProtoBase;
using System.Text;

namespace WindowsFormsApp1.service
{
    class MyBeginEndMarkReceiveFilter : BeginEndMarkReceiveFilter<StringPackageInfo>
    {
        public MyBeginEndMarkReceiveFilter(string begin, string end)
        : base(Encoding.UTF8.GetBytes(begin), Encoding.UTF8.GetBytes(end))
        {
            this.begin = begin;
            this.end = end;
        }
        string begin;
        string end;
        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            //获取接收到的完整数据，包括头和尾
            var body = bufferStream.ReadString((int)bufferStream.Length, Encoding.ASCII);
            //掐头去尾，只返回中间的数据
            body = body.Remove(body.Length - end.Length, end.Length);
            body = body.Remove(0, begin.Length);
            return new StringPackageInfo("", body, new string[] { });
        }
    }
}
