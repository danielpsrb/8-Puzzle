using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
*
*Do you know what is 8 puzzle? : https://d18l82el6cdm1i.cloudfront.net/solvable/65cf7a7e25.64c7ec8a92.Rb0KOG.png
*
*/
namespace _8_puzzle_C_Sharp_A_Star
{
    class node
    {
       public int[,] state = new int[3, 3];
    public string action = "";
    public int depth = 0;
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool found = false;
            int selected_depth = 0;

            int Max_Depth = 50;

            List<node> FrontHere = new List<node>();
            List<node> Checked = new List<node>();
            List<string> actions = new List<string>();

            int[,] GoalState = new int[,] {
                { 1 , 2 , 3 },
                { 4 , 5 , 6 },
                { 7 , 8 , 0 }
            };

            int[,] StartState = new int[,] {
                { 2 , 4 , 3 },
                { 1 , 8 , 5 },
                { 7 , 0 , 6 }
            };

            node start = new node() {
                state = StartState,
                depth = 0
            };
            FrontHere.Add(start);

            void print_state(int[,] state)
            {
                string to_print = "";

                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        to_print += state[j, i] + " ";
                    }
                    to_print += "\n";
                }
                Console.WriteLine(to_print);
            }


            bool can_right(int[,] state)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (state[i, 2] == 0)
                    {
                       return false;
                    }
                }
                return true;
            }
            bool can_left(int[,] state)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (state[i, 0] == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            bool can_up(int[,] state)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (state[0, i] == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            bool can_down(int[,] state)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (state[2, i] == 0)
                    {
                        return false;
                    }
                }
                return true;
            }



            int[,] right(int[,] state)
            {
                int[,] what = new int[,] {
                    { 1 , 2 , 3 },
                    { 4 , 5 , 6 },
                    { 7 , 0 , 8 }
                };
                for (int height = 0; height < 3; height++)
                {
                    for (int width = 0; width < 3; width++)
                    {
                        what[height, width] = state[height, width];
                    }
                }

                int j = zero_position(state, false);
                int i = zero_position(state);

                what[j, i] = state[j, i + 1];
                what[j, i + 1] = 0;

                return what;
            }
            int[,] left(int[,] state)
            {
                int[,] what = new int[,] {
                    { 1 , 2 , 3 },
                    { 4 , 5 , 6 },
                    { 7 , 0 , 8 }
                };
                for (int height = 0; height < 3; height++)
                {
                    for (int width = 0; width < 3; width++)
                    {
                        what[height, width] = state[height, width];
                    }
                }

                int j = zero_position(state, false);
                int i = zero_position(state);

                what[j, i] = state[j, i - 1];
                what[j, i - 1] = 0;

                return what;
            }
            int[,] up(int[,] state)
            {
                int[,] what = new int[,] {
                    { 1 , 2 , 3 },
                    { 4 , 5 , 6 },
                    { 7 , 0 , 8 }
                };
                for (int height = 0; height < 3; height++)
                {
                    for (int width = 0; width < 3; width++)
                    {
                        what[height, width] = state[height, width];
                    }
                }

                int j = zero_position(state, false);
                int i = zero_position(state);

                what[j, i] = state[j - 1, i];
                what[j - 1, i] = 0;

                return what;
            }
            int[,] down(int[,] state)
            {
                int[,] what = new int[,] {
                    { 1 , 2 , 3 },
                    { 4 , 5 , 6 },
                    { 7 , 0 , 8 }
                };
                for (int height = 0; height < 3; height++)
                {
                    for (int width = 0; width < 3; width++)
                    {
                        what[height, width] = state[height, width];
                    }
                }

                int j = zero_position(state, false);
                int i = zero_position(state);

                what[j, i] = state[j + 1, i];
                what[j + 1, i] = 0;

                return what;
            }

            int zero_position(int[,] state, bool width = true)
            {
                int what = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (state[j, i] == 0)
                        {
                            if (width == true)
                                what = i;
                            else
                                what = j;
                        }
                    }
                }
                return what;
            }

            bool was_checked(int[,] state)
            {
                bool what = false;

                foreach (node nd in Checked)
                {
                    if (match_array(nd.state, state))
                    {
                        what = true;
                    }
                }

                return what;
            }

            bool match_array(int[,] arr1, int[,] arr2)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (arr1[j, i] != arr2[j, i]) return false;
                    }
                }

                return true;
            }

            string get_action(int[,] state)
            {
                foreach (node nd in Checked)
                {
                    if (match_array(nd.state, state))
                    {
                        return nd.action;
                    }
                }
                return "none";
            }

            void print_path(int[,] state, string action)
            {
                int[,] current_state = state;

                if (action == "right")
                {
                    current_state = left(current_state);
                    actions.Add("right");
                }

                else if (action == "left")
                {
                    current_state = right(current_state);
                    actions.Add("left");
                }
                else if (action == "up")
                {
                    current_state = down(current_state);
                    actions.Add("up");
                }
                else if (action == "down")
                {
                    current_state = up(current_state);
                    actions.Add("down");
                }

                while (!match_array(current_state, StartState))
                {
                    string current_action = get_action(current_state);

                    if (current_action == "right")
                    {
                        current_state = left(current_state);
                        actions.Add("right");
                    }

                    else if (current_action == "left")
                    {
                        current_state = right(current_state);
                        actions.Add("left");
                    }
                    else if (current_action == "up")
                    {
                        current_state = down(current_state);
                        actions.Add("up");
                    }
                    else if (current_action == "down")
                    {
                        current_state = up(current_state);
                        actions.Add("down");
                    }
                }


                Console.WriteLine($"Steps ({actions.Count} Step):\n");
                print_state(StartState);

                for (int a = actions.Count - 1; a >= 0; a--)
                {

                    if (actions[a] == "right")
                    {
                        current_state = right(current_state);
                    }

                    else if (actions[a] == "left")
                    {
                        current_state = left(current_state);
                    }
                    else if (actions[a] == "up")
                    {
                        current_state = up(current_state);
                    }
                    else if (actions[a] == "down")
                    {
                        current_state = down(current_state);
                    }



                    print_state(current_state);
                }
            }

            int h(int[,] state)
            {
                int match = 0;

                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (state[j, i] == GoalState[j, i]) match++;
                    }
                }

                return 9 - match;
            }

            node get_state()
            {
                node what = new node();
                int index = 0;
                int selected_index = 0;
                int min_f = Max_Depth + 9;
                foreach (node nd in FrontHere)
                {
                    if (nd.depth + h(nd.state) < min_f)
                    {
                        min_f = nd.depth + h(nd.state);
                        what.state = nd.state;
                        what.depth = nd.depth;
                        what.action = nd.action;
                        selected_index = index;
                    }
                    index++;
                }

                FrontHere.RemoveAt(selected_index);

                return what;
            }




            Console.WriteLine("Start State:\n");
            print_state(StartState);
            Console.WriteLine("Goal State:\n");
            print_state(GoalState);


            while (FrontHere.Count != 0 && !found && selected_depth <= Max_Depth)
            {
                node best_result = get_state();
                if (match_array(best_result.state, GoalState))
                {
                    print_path(best_result.state, best_result.action);
                    found = true;
                }
                else
                {

                    if (can_right(best_result.state) && !was_checked(right(best_result.state)))
                    {
                        node data = new node();
                        data.depth = best_result.depth + 1;
                        data.state = right(best_result.state);
                        data.action = "right";

                        FrontHere.Add(data);
                    }

                    if (can_left(best_result.state) && !was_checked(left(best_result.state)))
                    {
                        node data = new node();
                        data.depth = best_result.depth + 1;
                        data.state = left(best_result.state);
                        data.action = "left";

                        FrontHere.Add(data);
                    }

                    if (can_up(best_result.state) && !was_checked(up(best_result.state)))
                    {
                        node data = new node();
                        data.depth = best_result.depth + 1;
                        data.state = up(best_result.state);
                        data.action = "up";

                        FrontHere.Add(data);
                    }

                    if (can_down(best_result.state) && !was_checked(down(best_result.state)))
                    {
                        node data = new node();
                        data.depth = best_result.depth + 1;
                        data.state = down(best_result.state);
                        data.action = "down";

                        FrontHere.Add(data);
                    }

                    selected_depth = best_result.depth;
                    Checked.Add(best_result);
                }
            }
            if (selected_depth > Max_Depth)
            {
                Console.WriteLine("Can't Solve!");
            }
            Console.ReadLine();
        }
    }
}