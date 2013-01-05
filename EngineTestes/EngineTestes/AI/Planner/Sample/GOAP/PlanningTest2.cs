using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Settlers.BlackBoardControl.GOAP;
using PloobsEngine.IA;
using Settlers.BlackBoardControl.GOAP.Actions;
using Settlers;
using PloobsEngine.TestSuite;
using System.Threading.Tasks;

namespace SettlersConsoleTests
{
    [TesteAlgorithmClass]
    class PlanningTest2
    {
        [TesteAlgorithmMethod]
        public void teste2()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            try
            {
                List<Task> task = new List<Task>();
                for (int i = 0; i < 1000; i++)
                {

                    Task t = Task.Factory.StartNew(
                        () =>
                        {
                            BlackBoard BlackBoard = new PloobsEngine.IA.BlackBoard();
                            BlackBoard.AddTrigger(new SimpleTrigger("trigger",
                                (a, b) =>
                                {
                                    return true;
                                }
                            ,
                            (a) =>
                            {
                                BlackBoard.SetEntry<int>("teste", 5);
                            }
                            ));

                            BlackBoard.SetEntry<int>("teste", 5);
                            BlackBoard.AddTrigger(new SimpleTrigger("trigger2",
                                (a, b) =>
                                {
                                    return true;
                                }
                            ,
                            (a) =>
                            {
                                BlackBoard.SetEntry<int>("teste", -5);
                            }
                            ));

                            BlackBoard.SetEntry<int>("teste", 1);
                            BlackBoard.SetEntry<int>("teste", 8);
                            BlackBoard.SetEntry<int>("teste", 2);

                            BlackBoard.AtomicOperateOnEntry<int>(
                                (a) =>
                                {
                                    a.SetEntry<int>("teste", 25);
                                    System.Diagnostics.Debug.Assert(a.GetEntry<int>("teste") == 25);
                                    a.SetEntry<int>("teste", 26);
                                    System.Diagnostics.Debug.Assert(a.GetEntry<int>("teste") == 26);
                                    a.SetEntry<int>("teste", 28);
                                    System.Diagnostics.Debug.Assert(a.GetEntry<int>("teste") == 28);
                                    a.SetEntry<int>("teste", 34);
                                    System.Diagnostics.Debug.Assert(a.GetEntry<int>("teste") == 34);
                                }
                            );

                        });
                    task.Add(t);
                }
                Task.WaitAll(task.ToArray());
            }
            catch (Exception)            
            {
                throw new NotImplementedException();
            }

            
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        [TesteAlgorithmMethod]
        public void teste()
        {
            BlackBoard BlackBoard = new PloobsEngine.IA.BlackBoard();
            WorldState WorldState = new PloobsEngine.IA.WorldState();
            WorldState.SetSymbolValue<int>("gold", 5);
            WorldState.SetSymbolValue<int>("wood", 5);
            WorldState.SetSymbolValue<int>("castle", 0);
            WorldState.SetSymbolValue<int>("warrior", 0);
            WorldState.SetSymbolValue<bool>("attack", false);
            BlackBoard.SetEntry<WorldState>("WorldState", WorldState);
            
            Dictionary<string, int> resneeded = new Dictionary<string, int>();
            resneeded.Add("gold",10);

            Dictionary<string, int> resneeded2 = new Dictionary<string, int>();
            resneeded2.Add("wood", 2);
            resneeded2.Add("gold", 2);

            Dictionary<string, int> unitneeded = new Dictionary<string, int>();
            unitneeded.Add("warrior", 3);

            List<PloobsEngine.IA.Action> actions = new List<PloobsEngine.IA.Action>();
            actions.Add(new GatherResource("gold",5));
            actions.Add(new GatherResource("wood", 5));
            actions.Add(new Build(resneeded, "castle", 1));
            actions.Add(new MakeUnit("warrior", "castle", resneeded2));
            actions.Add(new Patrol());
            actions.Add(new Attack(unitneeded));
            
            GoalPlanning GoalPlanning = new GoalPlanning(BlackBoard);
            PlanSet PlanSet = GoalPlanning.GetPlan(new GoalAttack(), actions);

            System.Diagnostics.Debug.Assert(PlanSet != null);

            foreach (var item in PlanSet.Actions)
            {
                Console.WriteLine(item.Name);
            }
        }

    }
}

