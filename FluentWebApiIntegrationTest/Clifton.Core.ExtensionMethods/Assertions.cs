using System;

namespace Clifton.Core.Assertions
{
    public static class Assertion
    {
        public static void That<T>(bool condition, string msg) where T : Exception, new()
        {
            if (!condition)
            {
                var ex = Activator.CreateInstance(typeof(T), new object[] { msg }) as T;
                throw ex;
            }
        }

        public static void NotNull<T>(object obj, string msg) where T : Exception, new()
        {
            if (obj == null)
            {
                var ex = Activator.CreateInstance(typeof(T), new object[] { msg }) as T;
                throw ex;
            }
        }

        public static void Try(Action f, Action onFail)
        {
            try
            {
                f();
            }
            catch
            {
                onFail();
            }
        }

        public static void SilentTry(Action f)
        {
            try
            {
                f();
            }
            catch { }
        }

        public static int? SilentTry(Func<int> f)
        {
            int? n = null;
            try
            {
                n = f();

                return n;
            }
            catch { }

            return n;
        }

        public static DateTime? SilentTry(Func<DateTime?> f)
        {
            DateTime? n = null;
            try
            {
                n = f();

                return n;
            }
            catch { }

            return n;
        }

        public static void NotThat(bool cond, string message, System.Action beforeException = null)
        {
            if (cond)
            {
                beforeException?.Invoke();
                throw new Exception(message);
            }
        }

        public static void That(bool cond, string message, Action beforeException = null)
        {
            if (!cond)
            {
                beforeException?.Invoke();
                throw new Exception(message);
            }
        }

        public static void NotNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new Exception(message);
            }
        }
    }
}
