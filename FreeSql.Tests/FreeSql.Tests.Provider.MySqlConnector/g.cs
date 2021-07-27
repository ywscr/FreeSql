﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


public class g
{

    static Lazy<IFreeSql> mysqlLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
        .UseConnectionString(FreeSql.DataType.MySql, "Data Source=127.0.0.1;Port=3306;User ID=root;Password=root;Initial Catalog=cccddd_mysqlconnector;Charset=utf8;SslMode=none;Max pool size=10;AllowLoadLocalInfile=true")
        //.UseConnectionFactory(FreeSql.DataType.MySql, () => new MySql.Data.MySqlClient.MySqlConnection("Data Source=127.0.0.1;Port=3306;User ID=root;Password=root;Initial Catalog=cccddd_mysqlconnector;Charset=utf8;SslMode=none;"))
        //.UseConnectionString(FreeSql.DataType.MySql, "Data Source=192.168.164.10;Port=33061;User ID=root;Password=root;Initial Catalog=cccddd_mysqlconnector;Charset=utf8;SslMode=none;Max pool size=10")
        .UseAutoSyncStructure(true)
        //.UseGenerateCommandParameterWithLambda(true)
        .UseMonitorCommand(
            cmd =>
            {
                cmd.CommandTimeout = 200000;
                Trace.WriteLine(cmd.CommandText);
            }, //监听SQL命令对象，在执行前
            (cmd, traceLog) =>
            {
                Console.WriteLine(traceLog);
            }) //监听SQL命令对象，在执行后
        .UseLazyLoading(true)
        .Build());
    public static IFreeSql mysql => mysqlLazy.Value;
}
