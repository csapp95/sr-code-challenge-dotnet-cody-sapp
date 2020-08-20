using challenge.Data;
using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{

    public class CompensationService:ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<ICompensationService> _logger;


        public CompensationService(ILogger<ICompensationService> logger, ICompensationRepository compensationRepo)
        {
            _compensationRepository = compensationRepo;
            _logger = logger;
        }

        public Compensation GetById(String id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }

        public Compensation Create(Compensation compensation)
        {
            if(compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation Replace(Compensation originalComp, Compensation newComp)
        {
            if(originalComp != null)
            {
                _compensationRepository.Remove(originalComp);

                if(newComp != null)
                {
                    _compensationRepository.SaveAsync().Wait();

                    _compensationRepository.Add(newComp);

                }
            }
            return newComp;
        }
    }
}
