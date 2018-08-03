namespace BusTickets.Client.Core.Commands
{
    using System;
    using System.Text;
    using Dtos;
    using Models;
    using Services.Contracts;

    public class PrintReviewsCommand : Command
    {
        private readonly IRepository<Company> companyRepository;

        public PrintReviewsCommand(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 1);

            if (!int.TryParse(data[0], out var companyId))
            {
                throw new ArgumentException("Invalid company's id format!");
            }

            var companyDto = this.companyRepository.GetById<CompanyReviewsDto>(companyId);
            this.EnsureNotNull(companyDto, "Company", companyId.ToString());

            return GetReviewsInfo(companyDto);
        }

        private static string GetReviewsInfo(CompanyReviewsDto companyDto)
        {
            var sb = new StringBuilder();
            foreach (var r in companyDto.Reviews)
            {
                sb.AppendLine($"Review ID: {r.Id} Grade: {r.Grade} Published on: {r.PublishedDateTime}")
                    .AppendLine($"   - from {r.CustomerFullName}")
                    .AppendLine($"   [{r.Content}]");
            }

            return sb.ToString().TrimEnd();
        }
    }

}
