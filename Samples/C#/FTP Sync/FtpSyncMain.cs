using System;

namespace FtpSync
{
    class FtpSyncMain
    {
        [STAThread]
        static void Main(string[] args)
        {
            FtpSyncWorker lWorker = new FtpSyncWorker();

            if (lWorker.CheckArgs(args))
            {
                lWorker.Sync();
            }
        }
    }
}
