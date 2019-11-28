using OpenTracing;
using OpenTracing.Util;
using Jaeger;
using Jaeger.Samplers;
using Jaeger.Reporters;
using Jaeger.Senders;
using System.Collections.Generic;
using JaegerTracer = Jaeger.Tracer;

namespace OpenTracingProj
{
   public class Tracer
   {
      private static ITracer _tracer;
      private static InMemoryReporter _inMemoryReporter;

      public static void ConfigureTracer()
      {
         ISender sender = new HttpSender($"http://localhost:3526");
         var reporter = new RemoteReporter.Builder().WithSender(sender).Build();

         _inMemoryReporter = new InMemoryReporter();

         _tracer = new JaegerTracer.Builder("Proscheduler")
            .WithReporter(_inMemoryReporter)
            .WithSampler(new ConstSampler(true))
            .Build();

         GlobalTracer.Register(_tracer);
      }

      public static ITracer Instance { get { return _tracer; } }

      public static IReadOnlyList<Span> GetInMemorySpans() //also we can clear in memory list
      {
         return _inMemoryReporter?.GetSpans();
      }
   }
}
