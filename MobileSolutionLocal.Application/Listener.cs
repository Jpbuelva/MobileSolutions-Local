using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace MobileSolutionLocal.Application
{
    public class Listener
    {

        public void GetFile()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["Source"]);
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            watcher.Created += watcher_Created;
            Console.ReadLine();

        }

        private static void watcher_Created(object sender, FileSystemEventArgs e)
        {

            var sourceDir = ConfigurationManager.AppSettings["Source"];
            var backupDir = ConfigurationManager.AppSettings["Destination"];
            try
            {
                var List = Directory.GetFiles(sourceDir, "*.pdf").Select(fn => new FileInfo(fn)).
                                                            OrderByDescending(f => f.Length).ToList();
                CopyFile(  backupDir, List);

            }
            catch (Exception)
            {

                throw;
            }


        }

        private static void CopyFile(  string backupDir, List<FileInfo> list)
        {
            try
            {
                // Copy picture files.
                foreach (var f in list)
                {
                    Console.WriteLine(f.Length);
                    File.Copy(Path.Combine(f.FullName), Path.Combine(backupDir, f.Name), true);
                }
                foreach (var f in list)
                {
                    File.Delete(f.FullName);
                }
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);


            }
        }

       
    }
}
