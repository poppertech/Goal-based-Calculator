using PoppertechCalculator.Models.PSO;
namespace PoppertechCalculator.Logic.Interfaces.Pso
{
    public interface IPsoCalculationsProcessor
    {
        PsoResults OptimizePortfolio(PsoContext context);
    }
}
