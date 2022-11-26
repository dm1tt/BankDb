using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank;

public interface IQueryTool
{
    public int InsertQuery(string fullName, string phone, string requisites, string city, string country);
    public void DeleteQuery(int userId);
    public void ListCompletion();
    public void GlobalSelectQuery();
    public void SingleSelectQuery(int userId);
    public void UpdateQuery(int userId, int userAnswer, string newUserData);
    public void UpdateQuery(int userId, string newCity, string newCountry);

}
