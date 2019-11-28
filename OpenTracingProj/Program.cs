using OpenTracing;
using System.Threading;

namespace OpenTracingProj
{
   class Program
   {
      static void Main(string[] args)
      {
         Tracer.ConfigureTracer();

         IScope scope = Tracer.Instance.BuildSpan("Main").WithTag("skills", "23").StartActive(true);
         SomeMethod1();
         Tracer.Instance.ScopeManager.Active?.Dispose();

         var spans = Tracer.GetInMemorySpans();
      }


      static void SomeMethod1()
      {
         using(IScope scop = Tracer.Instance.BuildSpan("method22222").StartActive(true))
         {
            Thread.Sleep(2000);
            SomeMethod2();
         }        
      }

      static void SomeMethod2()
      {
         Thread.Sleep(1000);
      }
   }
}
