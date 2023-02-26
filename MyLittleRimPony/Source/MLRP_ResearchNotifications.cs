using RimWorld;
using System.Collections.Generic;
using Verse;

namespace ResearchFinished
{
    public class ResearchFinished : GameComponent
    {
        private List<ResearchProjectDef> monitoredProjects = new List<ResearchProjectDef>();
        private HashSet<string> completedProjects = new HashSet<string>();

        public ResearchFinished(Game game)
        {
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            // Add the defNames of the research projects you want to monitor to the list.
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("Brewing"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("Fabrication"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("AdvancedFabrication"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("ShipComputerCore"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("MedicineProduction"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("DrugProduction"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("Mortars"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("MLRP_MagicMirrorResearch"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("Bionics"));
            monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("Devilstrand"));

            // If the user doesn't have Royalty, we need to monitor this research. as well.
            if (!ModsConfig.IsActive("Ludeon.RimWorld.Royalty"))
            {
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("HospitalBed"));
            }

            if (ModsConfig.IsActive("Ludeon.RimWorld.Royalty"))
            {
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("HealingFactors")); // Replaces Hospital Bed research.
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("NeuralComputation"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("MolecularAnalysis"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("SkinHardening"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("FleshShaping"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("ArtificialMetabolism"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("CircadianInfluence"));
            }

            if (ModsConfig.IsActive("Ludeon.RimWorld.Biotech"))
            {
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("StandardMechtech"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("HighMechtech"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("UltraMechtech"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("Deathrest"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("ToxFiltration"));
            }

            // New recipes are added if certain mods are enabled.

            if (ModsConfig.IsActive("sumghai.Replimat")) // Replimat
            {
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("MolecularNutrientResequencing"));
            }

            if (ModsConfig.IsActive("sumghai.medpod")) // MedPod
            {
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("AcceleratedCellularRegeneration"));
            }

            if (ModsConfig.IsActive("hlx.ReinforcedMechanoids2")) // Reinforced Mechanoids 2
            {
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("RM_WeatherController"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("RM_ClimateAdjuster"));
                monitoredProjects.Add(DefDatabase<ResearchProjectDef>.GetNamed("RM_SunBlocker"));
            }
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();
            foreach (var project in monitoredProjects)
            {
                if (completedProjects.Contains(project.defName))
                {
                    return;
                }
                else if (!completedProjects.Contains(project.defName) && project.IsFinished)
                {
                if (project.defName == "Brewing" || project.defName == "Fabrication" || project.defName == "AdvancedFabrication")
                {
                    Find.LetterStack.ReceiveLetter("MLRP_RecipeUnlockedLetterHeader".Translate(), $"MLRP_RecipeUnlockedLetterText_DB".Translate(), LetterDefOf.PositiveEvent);
                }
                if (project.defName == "Devilstrand")
                {
                    Find.LetterStack.ReceiveLetter("MLRP_RecipeUnlockedLetterHeader".Translate(), $"MLRP_RecipeUnlockedLetterText_FE".Translate(), LetterDefOf.PositiveEvent);
                }
                else
                {
                    Find.LetterStack.ReceiveLetter("MLRP_RecipeUnlockedLetterHeader".Translate(), $"MLRP_RecipeUnlockedLetterText_NMM".Translate(), LetterDefOf.PositiveEvent);
                }
                    completedProjects.Add(project.defName);
                }
            }
        }
    }
}
