using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using System.Linq;

namespace RestAPI.Services
{
    public interface IResultService
    {
        List<Result> GetResult();
    }
    public class ResultService : IResultService
    {
        private readonly ResultContext _contextResult;
        private readonly CandidateContext _contextCandidate;
        private readonly CompanyContext _contextCompany;

        public ResultService(ResultContext resultContext,
                             CandidateContext candidateContext,
                             CompanyContext companyContext)
        {
            _contextResult = resultContext;
            _contextCandidate = candidateContext;
            _contextCompany = companyContext;
        }

        public List<Result> GetResult()
        {

            var companyList = _contextCompany
                                .CompanyItems
                                .Where(w => w.AvailableJobOpportunity)
                                .ToList();

            var candidateList = _contextCandidate
                .CandidateItems
                .ToList();

            List<Result> resultList = new List<Result>();

            foreach (var company in companyList)
            {
                foreach (var candidate in candidateList)
                {
                    if (candidate.Technologys.Any(company.Technology.Contains))
                    {
                        resultList.Add(new Result
                        {
                            CandidateID = candidate.Id,
                            CandidatePoints = company.TechnologyValue
                        });
                    }
                }
            }

            resultList = resultList.GroupBy(g => g.CandidateID)
                    .Select(s => new Result {
                        CandidateID = s.Key,
                        CandidatePoints = s.Sum(i => i.CandidatePoints)
                    }).ToList();

            return resultList.OrderBy(o => o.CandidateID).ToList();
        }
    }
}
