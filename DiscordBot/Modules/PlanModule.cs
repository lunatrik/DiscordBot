using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Skills.Web;
using Microsoft.SemanticKernel.Skills.Web.Bing;

namespace DiscordBot.Modules
{
    public class PlanModule : InteractionModuleBase<SocketInteractionContext>
    {
        private static ILogger _logger;
        private static IConfiguration _configuration;

        public PlanModule(ILogger<PlanModule> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [SlashCommand("ask", "ask to bot")]
        public async Task Ask(string goal)
        {
            //perform a web search to the last space X launch in which the rocket exploded for write a twitter post in reference.
            await DeferAsync();
            var kernel = new KernelBuilder().WithLogger(_logger).Build();
            var key = _configuration["OPENAI_API_KEY"];
            kernel.Config.AddOpenAITextCompletionService("text-davinci-002", key);


            string folder = RepoFiles.SampleSkillsPath();
            kernel.ImportSemanticSkillFromDirectory(folder, "SummarizeSkill");
            kernel.ImportSemanticSkillFromDirectory(folder, "WriterSkill");

            var bingKey = _configuration["BING_API_KEY"];

            using var bingConnector = new BingConnector(bingKey);
            kernel.ImportSkill(new WebSearchEngineSkill(bingConnector), "bing");

            var planner = new ActionPlanner(kernel);

            // The planner returns a plan, consisting of a single function
            // to execute and achieve the goal requested.
            var plan = await planner.CreatePlanAsync(goal);

            // Execute the full plan (which is a single function)
            SKContext result = await plan.InvokeAsync();

            // Show the result, which should match the given goal
            Console.WriteLine(result);

            await FollowupAsync(result.ToString());




        }


    }
}
