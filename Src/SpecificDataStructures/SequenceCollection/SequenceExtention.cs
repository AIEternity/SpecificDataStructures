using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceCollection
{
    public static class SequenceExtention
    {
        

        public static ISequence<T> Slice<T>(this ISequence<T> first, (int? begin, int? end, int? step) index)
        {
            if (index.begin == null)
                index.begin = 0;
            if (index.end == null)
                index.end = first.Count;
            if (index.step == null)
                index.step = 1;
            if (index.begin < 0)
                index.begin = first.Count + index.begin;
            if (index.end < 0)
                index.end = index.end + first.Count;

            if (index.begin < 0)
                index.begin = 0;
            if (index.end < 0)
                index.end = first.Count;

            
            return new LazySequence<T>(i => first[(int)index.begin + (int)index.step * i], (int)Math.Round(((int)index.end - ((int)index.begin)) / (double)((int)index.step)));
        }

        public static ISequence<T> Slice<T>(this ISequence<T> first, (int? begin, int? end) index)
        {
            return Slice(first, (index.begin, index.end, 1));
        }
      
        public static ISequence<T> Delay<T>(this ISequence<T> first, int delay)
        {
            return new LazySequence<T>((i) => first[i - delay], first.Count);
        }

        public static ISequence<ISequence<T>> SplitSequence<T>(this ISequence<T>  lst,int count)
        {
            List<ISequence<T>> sqnc = new List<ISequence<T>>();
            int counter = 0;
            List<T> sub = new List<T>();
            for (int i = 0; i < lst.Count; i++)
            {
                sub.Add(lst[i]);
                counter++;
                if (counter == count)
                {
                    counter = 0;
                    sqnc.Add(new Sequence<T>(sub));
                    sub = new List<T>();
                }
            }
            if (sub.Count > 0)
                sqnc.Add(new Sequence<T>(sub));
            return new Sequence<ISequence<T>>(sqnc);
        }

        public static ISequence<double> Normolize(this ISequence<double> first)
        {
            double mean = first.Average(x => x);
            double sigma = Math.Sqrt((1.0 / (double)first.Count()) * first.Sum(x => (x - mean) * (x - mean)));
            return new LazySequence<double>((i) => 1.0 / (1 + Math.Exp(-(first[i] - mean) / sigma)), first.Count);          
        }

        public static ISequence<ISequence<T>> Join<T>(params ISequence<T>[] lst)
        {
            return new LazySequence<ISequence<T>>(i => new Sequence<T>(lst.Select(x=>x[i]).ToArray()), lst[0].Count);
        }

        public static ISequence<T> Join<T>(this ISequence<ISequence<T>> lst)
        {
            return new LazySequence<T>(index=>lst[index][0],lst.Count());
        }

        public static ISequence<double> Velocity(this ISequence<double> first, int period)
        {
            return first.Subtract(first.Delay(period)).Divide(period);
        }

        public static ISequence<double> PercentVelocity(this ISequence<double> first, int period)
        {
            return first.Subtract(first.Delay(period)).Divide(first.Delay(period));
        }

        public static ISequence<double> Max(this ISequence<double> first, int period)
        {
            return new LazySequence<double>((i) => first.Slice((Math.Max(0, i - period + 1), Math.Max(i + 1, 1))).Max(x => x), first.Count);
        }

        public static ISequence<double> Min(this ISequence<double> first, int period)
        {
            return new LazySequence<double>((i) => first.Slice((Math.Max(0, i - period + 1), Math.Max(i + 1, 1))).Min(x => x), first.Count);
        }

        public static ISequence<double> Min(this ISequence<double> first, params ISequence<double>[] sequences)
        {
            return new LazySequence<double>((i) =>
            {
                return sequences.Union(new[] { first }).Min(x => x[i]);
            }, first.Count);
        }

        public static ISequence<double> Max(this ISequence<double> first, params ISequence<double>[] sequences)
        {
            return new LazySequence<double>((i) =>
            {
                return sequences.Union(new[] { first }).Max(x => x[i]);
            }, first.Count);
        }

        public static double Max(this ISequence<double> first)
        {
            int count = first.Count();
            double max = first[0];
            for (int i = 1; i < count; i++)
                if (first[i] > max)
                    max = first[i];
            return max;

        }

        public static double Min(this ISequence<double> first)
        {
            int count = first.Count();
            double min = first[0];
            for (int i = 1; i < count; i++)
                if (first[i] < min)
                    min = first[i];
            return min;
        }

        public static ISequence<double> Avg(this ISequence<double> first, ISequence<double> second)
        {
            if (first.Count != second.Count)
                throw new ArgumentOutOfRangeException();
            return new LazySequence<double>((i) => (first[i] + second[i]) / 2.0, first.Count);
        }

        public static ISequence<double> Subtract(this ISequence<double> first, ISequence<double> second)
        {
            return new LazySequence<double>((i) => first[i] - second[i], first.Count);
        }

        public static ISequence<double> Add(this ISequence<double> first, ISequence<double> second)
        {
            return new LazySequence<double>((i) => first[i] + second[i], first.Count);
        }

        public static ISequence<double> Multiple(this ISequence<double> first, ISequence<double> second)
        {
            return new LazySequence<double>((i) => first[i] * second[i], first.Count);
        }

        public static ISequence<double> Sum(this ISequence<double> first, int period)
        {
            return new LazySequence<double>((i) =>
            {
                var slice = first.Slice((Math.Max(0, i - period + 1), Math.Max(i + 1, 1)));
                return slice.Sum();
            }, first.Count);
        }

        public static ISequence<double> Divide(this ISequence<double> first, ISequence<double> second)
        {
            return new LazySequence<double>((i) =>
            {
                if (second[i] == 0) return 0;
                return first[i] / second[i];
            }, first.Count);
        }

        public static ISequence<double> Divide(this ISequence<double> first, double n)
        {
            return new LazySequence<double>((i) =>
            {
                if (n == 0) return 0;
                return first[i] / n;
            }, first.Count);
        }

        public static ISequence<double> Multiple(this ISequence<double> first, double n)
        {
            return new LazySequence<double>((i) =>
            {
                return first[i] * n;
            }, first.Count);
        }

        public static ISequence<double> MaxOrZero(this ISequence<double> first)
        {
            return new LazySequence<double>((i) => Math.Max(first[i], 0), first.Count);
        }

        public static ISequence<double> Abs(this ISequence<double> first)
        {
            return new LazySequence<double>((i) => Math.Abs(first[i]), first.Count);
        }

        public static ISequence<double> MinOrZeroAbs(this ISequence<double> first)
        {
            return new LazySequence<double>((i) =>
            {

                return Math.Abs(Math.Min(first[i], 0));
            }, first.Count);
        }

        public static ISequence<double> Sign(this ISequence<double> first)
        {
            return new LazySequence<double>((i) => Math.Sign(first[i]), first.Count);
        }

        public static Sequence<T> AsSequence<T>(this ISequence<T> sequence)
        {
            return new Sequence<T>(sequence);
        }

    }
}
