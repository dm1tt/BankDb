using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank;

public interface IQueryTool
{
    public string InsertQuery(string fullName, string phone, string requisites, string city, string country);
    public void DeleteQuery(int userId);
    public void ListCompletion();
    public void GlobalSelectQuery();
    public void SingleSelectQuery(int userId);

}
