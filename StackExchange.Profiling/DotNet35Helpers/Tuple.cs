using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if CSHARP30
	namespace System
	{
		public class Tuple<T1, T2> : IEqualityComparer<Tuple<T1, T2>>
		{
			public T1 First { get; private set; }
			public T2 Second { get; private set; }
			internal Tuple(T1 first, T2 second)
			{
				First = first;
				Second = second;
			}

			public override bool Equals(object obj)
			{
				if(obj is Tuple<T1,T2>)
				{
					var other = (Tuple<T1, T2>)obj;
					return (SafeEqual(this.First, other.First)
							&& SafeEqual(this.Second, other.Second));
				}
				else 
				{
					return base.Equals(obj);
				}
			}

			private static bool SafeEqual<T>(T left, T right)
			{
				if(left == null && right == null)
				{
					return true;
				}
				else if(left != null && right != null)
				{
					return left.Equals(right);
				}
				else 
				{
					return false;
				}
			}

			public override int GetHashCode()
			{
				if(this.First == null && this.Second == null)
				{
					return 0;
				}
				else if (this.First != null && this.Second == null)
				{
					return this.First.GetHashCode();
				}
				else if (this.First == null && this.Second != null)
				{
					return this.Second.GetHashCode();
				}
				else 
				{
					return (this.First.GetHashCode()/2 + this.Second.GetHashCode()/2);
				}
			}

			public bool Equals(Tuple<T1, T2> x, Tuple<T1, T2> y)
			{
				return (SafeEqual(x.First, y.First) && SafeEqual(x.Second, y.Second));
			}

			public int GetHashCode(Tuple<T1, T2> obj)
			{
				return 0;
			}
		}

		public static class Tuple 
		{
			public static Tuple<T1, T2> Create<T1, T2>(T1 first, T2 second)
			{
				var tuple = new Tuple<T1, T2>(first, second);
				return tuple;
			}
		}
	}
#endif
