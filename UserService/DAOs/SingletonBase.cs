using UserService.DAOs;

namespace UserService.DAOs
{
    public class SingletonBase<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object _lock = new object();
        public static LitopiaUserServiceDbContext _context { get; set; } = new LitopiaUserServiceDbContext();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                    return _instance;
                }
            }
            set { _instance = value; }
        }
    }
}
