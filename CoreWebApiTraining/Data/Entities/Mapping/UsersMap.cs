using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Data.Entities.Mapping
{
    public class UsersMap : ClassMapping<Users>
    {
        public UsersMap()
        {
            Id(x => x.Id, x =>
              {
                  x.Generator(Generators.Increment);
                  x.Type(NHibernateUtil.Int32);
                  x.Column("ID");
                  x.UnsavedValue(default(int));
              });

            Property(x => x.Name, x =>
               {
                   x.Length(50);
                   x.Type(NHibernateUtil.String);
                   x.Column("NAME");
               });

            Property(x => x.Surname, x =>
              {
                  x.Length(50);
                  x.Type(NHibernateUtil.String);
                  x.Column("SURNAME");
              });

            Property(x => x.Email, x =>
               {
                   x.Length(100);
                   x.Type(NHibernateUtil.String);
                   x.Column("EMAIL");
               });

            Property(x => x.Address, x =>
              {
                  x.Length(100);
                  x.Type(NHibernateUtil.String);
                  x.Column("ADDRESS");
              });

            Schema("sc_app");

            Table("users");
        }
    }
}
