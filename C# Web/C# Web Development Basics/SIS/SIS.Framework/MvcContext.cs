namespace SIS.Framework
{
    public class MvcContext
    {
        private static MvcContext instance;
        private static readonly object InstanceLock = new object();

        private MvcContext() { }

        //Thread-safety
        public static MvcContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new MvcContext();
                        }
                    }
                }

                return instance;
            }
        }

        public string AssemblyName { get; set; }

        public string ControllersFolder { get; set; }

        public string ControllersSuffix { get; set; }

        public string ViewsFolder { get; set; }

        public string ModelsFolder { get; set; }

        public string ResourcesFolder { get; set; }
    }
}
