using System;
using System.Collections.Generic;
using System.Linq;
using Timetable_DateSheet_Generator.Data.Repositories.Algorithm;

namespace Timetable_DateSheet_Generator.Data.Algorithms.GeneticAlgorithmForTimetable
{
    public class GeneticAlgorithm
    {
        private readonly AlgorithmTimeTableRepository _Service;
        private int InstituteID;
        private int SemesterID;
        private int TimetableID;
        Chromosome fittestChromosome;
        private List<string> Results { get; set; }
        public string Message = "", MessageType = "";
        List<Chromosome> Population { get; set; }
        const int MAX_POPULATION_SIZE = 5;
        double requiredFitness = 75;
        public GeneticAlgorithm(AlgorithmTimeTableRepository algorithmTimeTableRepository, int InstituteID, int SemesterID, int TimetableID)
        {
            _Service = algorithmTimeTableRepository;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.InstituteID = InstituteID;
            this.SemesterID = SemesterID;
            this.TimetableID = TimetableID;
            fittestChromosome = new Chromosome(_Service, this.TimetableID);

            if (_Service.IsExists_Timetable(TimetableID))
                _Service.RemoveRange_CourseTimeSlots(TimetableID);
            Population = new List<Chromosome>();
            Results = new List<string>();
        }
        public List<string> getAllResults()
        {
            return Results;
        }
        public string GetFittestResult()
        {
            if (fittestChromosome.ChromosomeFitness <= 0)
                return "Not Found";
            return fittestChromosome.ChromosomeFitness.ToString() + " %";
        }
        public void GenerateTimeTable()
        {
            /// First Step...
            InitializePopulation();
            /// 2nd Step...
            /// 
            Selection();
            crossOverAndMuation();
            Selection();
            fittestChromosome.saveSolution();
        }
        public Chromosome getFittestChromosome()
        {
            return fittestChromosome;
        }
        public void Selection()
        {
            var LowFitnessChromosomes = Population.Where(c => c.ChromosomeFitness <= 0).ToList();
            foreach (var lowFitnessChromosome in LowFitnessChromosomes)
                Population.Remove(lowFitnessChromosome);
            Population = Population.OrderByDescending(c => c.ChromosomeFitness).ToList();
            if (Population.Count > 0)
            {
                fittestChromosome = Population.ElementAt(0);
                if (Population.Count > MAX_POPULATION_SIZE)
                    Population.RemoveRange(MAX_POPULATION_SIZE, Population.Count - MAX_POPULATION_SIZE);
                Message = "Timetable Generated Successfully.";
                MessageType = "Success";
            }
            else
            {
                Message = "Timetable Not Generated.";
                MessageType = "Fail";
            }

        }
        public void crossOverAndMuation()
        {
            List<Chromosome> NewOffSprings = new List<Chromosome>();
            int iteration = 0;
            do
            {
                NewOffSprings.Clear();
                for (int i = 1; i < Population.Count; i += 2)
                {
                    if (Population[i - 1].ChromosomeFitness == 100 || Population[i].ChromosomeFitness == 100)
                        break;
                    var OffSpring = Chromosome.CrossOver(Population[i - 1], Population[i]);
                    if (OffSpring != null && OffSpring.NewGenerated)
                        NewOffSprings.Add(Chromosome.CrossOver(Population[i - 1], Population[i]));
                    if (OffSpring != null && OffSpring.ChromosomeFitness == 100)
                        break;
                }
                Population.AddRange(NewOffSprings);
                setResult(NewOffSprings, "Crossover");
                NewOffSprings.Clear();
                Selection();
                foreach (var chromosome in Population)
                {
                    if (chromosome.ChromosomeFitness == 100)
                        break;
                    var OffSpring = Chromosome.Mutation(chromosome);
                    if (OffSpring != null && OffSpring.NewGenerated)
                        NewOffSprings.Add(Chromosome.Mutation(chromosome));
                    if (OffSpring != null && OffSpring.ChromosomeFitness == 100)
                        break;
                }
                setResult(NewOffSprings, "Mutation");
                Population.AddRange(NewOffSprings);
                Selection();
                iteration += 1;
                if (iteration >= MAX_POPULATION_SIZE)
                    break;

            } while (fittestChromosome.ChromosomeFitness < requiredFitness);
        }
        public void setResult(List<Chromosome> Chromosomes, string Step)
        {
            foreach (var chromosome in Chromosomes)
            {
                if (chromosome != null && chromosome.NewGenerated)
                {
                    if (chromosome.ChromosomeFitness <= 0)
                        Results.Add("Low Fitness. (" + Step + ")");
                    else
                        Results.Add(chromosome.ChromosomeFitness.ToString() + " % (" + Step + ")");
                }
            }
        }
        public void InitializePopulation()
        {
            for (int i = 0; i < MAX_POPULATION_SIZE; i++)
            {
                Chromosome chromosome = new Chromosome(_Service, InstituteID, SemesterID, TimetableID);
                Population.Add(chromosome);
                if (chromosome.ChromosomeFitness == 100)
                    break;
            }
            setResult(Population, "Initial");
        }

    }
}
