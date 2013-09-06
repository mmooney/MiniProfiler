using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackExchange.Profiling.Helpers.Dapper
{
    public class SqlBuilder
    {
        Dictionary<string, Clauses> data = new Dictionary<string, Clauses>();
        int seq;

        class Clause
        {
            public string Sql { get; set; }
            public object Parameters { get; set; }
        }

        class Clauses : List<Clause>
        {
            string joiner;
            string prefix;
            string postfix;

            public Clauses(string joiner, string prefix = "", string postfix = "")
            {
                this.joiner = joiner;
                this.prefix = prefix;
                this.postfix = postfix;
            }

            public string ResolveClauses(DynamicParameters p)
            {
                foreach (var item in this)
                {
                    p.AddDynamicParams(item.Parameters);
                }
                return prefix + string.Join(joiner, this.Select(c => c.Sql).ToArray()) + postfix;
            }
        }

        public class Template
        {
            readonly string sql;
            readonly SqlBuilder builder;
            readonly object initParams;
            int dataSeq = -1; // Unresolved

#if !CSHARP30
			public Template(SqlBuilder builder, string sql, dynamic parameters)
#else 
			public Template(SqlBuilder builder, string sql, object parameters)
#endif 
			{
                this.initParams = parameters;
                this.sql = sql;
                this.builder = builder;
            }

            static System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(@"\/\*\*.+\*\*\/", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.Multiline);

            void ResolveSql()
            {
                if (dataSeq != builder.seq)
                {
                    DynamicParameters p = new DynamicParameters(initParams);

                    rawSql = sql;

                    foreach (var pair in builder.data)
                    {
                        rawSql = rawSql.Replace("/**" + pair.Key + "**/", pair.Value.ResolveClauses(p));
                    }
                    parameters = p;

                    // replace all that is left with empty
                    rawSql = regex.Replace(rawSql, "");

                    dataSeq = builder.seq;
                }
            }

            string rawSql;
            object parameters;

            public string RawSql { get { ResolveSql(); return rawSql; } }
            public object Parameters { get { ResolveSql(); return parameters; } }
        }


        public SqlBuilder()
        {
        }

#if !CSHARP30
        public Template AddTemplate(string sql, dynamic parameters = null)
#else
		public Template AddTemplate(string sql, object parameters = null)
#endif
        {
            return new Template(this, sql, parameters);
        }

        void AddClause(string name, string sql, object parameters, string joiner, string prefix = "", string postfix = "")
        {
            Clauses clauses;
            if (!data.TryGetValue(name, out clauses))
            {
                clauses = new Clauses(joiner, prefix, postfix);
                data[name] = clauses;
            }
            clauses.Add(new Clause { Sql = sql, Parameters = parameters });
            seq++;
        }


#if !CSHARP30
        public SqlBuilder LeftJoin(string sql, dynamic parameters = null)
#else
		public SqlBuilder LeftJoin(string sql, object parameters = null)
#endif
        {
            AddClause("leftjoin", sql, parameters, joiner: "\nLEFT JOIN ", prefix: "\nLEFT JOIN ", postfix: "\n");
            return this;
        }

#if !CSHARP30
        public SqlBuilder Where(string sql, dynamic parameters = null)
#else
        public SqlBuilder Where(string sql, object parameters = null)
#endif
		{
            AddClause("where", sql, parameters, " AND ", prefix: "WHERE ", postfix: "\n");
            return this;
        }

#if !CSHARP30
        public SqlBuilder OrderBy(string sql, dynamic parameters = null)
#else
		public SqlBuilder OrderBy(string sql, object parameters = null)
#endif
        {
            AddClause("orderby", sql, parameters, " , ", prefix: "ORDER BY ", postfix: "\n");
            return this;
        }

#if !CSHARP30
        public SqlBuilder Select(string sql, dynamic parameters = null)
#else
		public SqlBuilder Select(string sql, object parameters = null)
#endif
        {
            AddClause("select", sql, parameters, " , ", prefix: "", postfix: "\n");
            return this;
        }

#if !CSHARP30
        public SqlBuilder AddParameters(dynamic parameters)
#else
		public SqlBuilder AddParameters(object parameters)
#endif
        {
            AddClause("--parameters", "", parameters, "");
            return this;
        }

#if !CSHARP30
        public SqlBuilder Join(string sql, dynamic parameters = null)
#else
		public SqlBuilder Join(string sql, object parameters = null)
#endif
        {
            AddClause("join", sql, parameters, joiner: "\nJOIN ", prefix: "\nJOIN ", postfix: "\n");
            return this;
        }

#if !CSHARP30
        public SqlBuilder GroupBy(string sql, dynamic parameters = null)
#else
		public SqlBuilder GroupBy(string sql, object parameters = null)
#endif
        {
            AddClause("groupby", sql, parameters, joiner: " , ", prefix: "\nGROUP BY ", postfix: "\n");
            return this;
        }

#if !CSHARP30
        public SqlBuilder Having(string sql, dynamic parameters = null)
#else 
        public SqlBuilder Having(string sql, object parameters = null)
#endif
        {
            AddClause("having", sql, parameters, joiner: "\nAND ", prefix: "HAVING ", postfix: "\n");
            return this;
        }
    }
}
