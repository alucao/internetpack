using System;
using System.IO;

namespace RemObjects.InternetPack.Ftp.VirtualFtp
{

  /*
   * - Keep files open
   * - use different Key and IV per file
   * 
   * 
   * 
   */

  public class SecureFolder: VirtualFolder
  {
    public SecureFolder(IFtpFolder aParent, string aName, SecureStorage aStorage) : base(aParent, aName)
    {
      fStorage = aStorage;
      WorldRead = true;
      WorldWrite = true;
      OwningUser = "system";
      OwningGroup = "system";
    }

    public SecureFolder(IFtpFolder aParent, string aName, SecureStorage aStorage, bool aPrivcate) : this(aParent, aName, aStorage)
    {
      fStorage = aStorage;
      GroupRead = false;
      GroupWrite = false;
      WorldRead = false;
      WorldWrite = false;
      OwningUser = "system";
      OwningGroup = "system";
    }

    protected override IFtpFolder DoCreateFolder(string aFolderName, VirtualFtpSession aSession)
      {
        if (HasSubfolder(aFolderName))
          throw new FtpException(String.Format("Cannot create folder named \"{0}\", a folder with this name already exists.",aFolderName));
          throw new FtpException(550, String.Format("Cannot create folder named \"{0}\", permission denied.",aFolderName));
    protected override IFtpFile DoCreateFile(string aFilename, VirtualFtpSession aSession)
        throw new FtpException(String.Format("Cannot create file \"{0}\", a file with this name already exists.",aFilename));
        throw new FtpException(550, String.Format("Cannot create file \"{0}\", permission denied.",aFilename));
    private SecureStorage fStorage;

}