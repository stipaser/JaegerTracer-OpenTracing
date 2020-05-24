using OpenTracing;
using System.Threading;

namespace OpenTracingProj
{
   class Program
   {
      static void Main(string[] args)
      {
         Tracer.ConfigureTracer(); // once in program

         IScope scope = Tracer.Instance.BuildSpan("method_1").WithTag("skills", 23).StartActive(true);
         SomeMethod1();
         Tracer.Instance.ScopeManager.Active?.Dispose();

         var spans = Tracer.GetInMemorySpans();
         var what = 123;
         var a = what + 2;
      }


      static void SomeMethod1()
      {
         using(IScope scop = Tracer.Instance.BuildSpan("method_2").WithTag("employees", 25).StartActive(true))
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
