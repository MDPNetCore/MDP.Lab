using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.BlazorCore.Lab
{
    public class TestService : InteropService
    {
        // Methods
        [AllowAnonymous]
        [InteropRoute("Sites/{companyId}/Test/Action1")]
        public async Task<TestResultModel> Action1(TestActionModel actionModel)
        {
            return await Task.FromResult(new TestResultModel());
        }

        public class TestActionModel
        {
            // Properties

        }

        public class TestResultModel
        {
            // Properties

        }
    }
}
