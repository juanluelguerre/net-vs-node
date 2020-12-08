using System;
using System.IO;
using System.Reactive.Linq;

namespace ElGuerre.RxConsole
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229241(v=vs.103)
    /// </summary>
    public class FileEventsManager
    {
        private FileSystemWatcher _fsw;

        public IObservable<FileSystemEventArgs> Created { get; private set; }
        public IObservable<RenamedEventArgs> Renamed { get; private set; }
        public IObservable<FileSystemEventArgs> Deteleted { get; private set; }

        /// <summary>
        /// Create a FileSystemWatcher to watch the <PATH> directory using the default NotifyFilter watching for changes to any type of file. 
        /// </summary>
        public FileEventsManager(string path)
        {
            _fsw = new FileSystemWatcher(path, "*.*")
            {
                EnableRaisingEvents = true
            };

            InitializeWatcher();
        }

        private void InitializeWatcher()
        {
            // Use the FromEvent operator to setup a subscription to the CREATED event.
            Created = Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(handler =>
                {
                    FileSystemEventHandler fsHandler = (sender, e) =>
                    {
                        handler(e);
                    };

                    return fsHandler;
                },
                fsHandler => _fsw.Created += fsHandler,
                fsHandler => _fsw.Created -= fsHandler
            );          

            //  Use the FromEvent operator to setup a subscription to the RENAMED event.
            Renamed = Observable.FromEvent<RenamedEventHandler, RenamedEventArgs>(handler =>
                {
                    RenamedEventHandler fsHandler = (sender, e) =>
                    {
                        handler(e);
                    };

                    return fsHandler;
                },
                fsHandler => _fsw.Renamed += fsHandler,
                fsHandler => _fsw.Renamed -= fsHandler
            );

            // Use the FromEvent operator to setup a subscription to the DELETED event.
            Deteleted = Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(handler =>
                {
                    FileSystemEventHandler fsHandler = (sender, e) =>
                    {
                        handler(e);
                    };

                    return fsHandler;
                },
                fsHandler => _fsw.Deleted += fsHandler,
                fsHandler => _fsw.Deleted -= fsHandler
            );
        }

    }
}
