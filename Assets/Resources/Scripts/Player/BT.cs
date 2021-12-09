using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT_lib
{
    public enum NODE_TYPE { ACTION, SELECTOR, SEQUENCE, INVERTER }

    public enum BT_VALUE { SUCCESS, FAIL, RUNNING }

    public delegate BT_VALUE ActionFuntion();
    public class BT
    {

        NODE_TYPE type;



        List<BT> children;

        ActionFuntion actionFunction;
        public BT(NODE_TYPE type, params BT[] _children)
        {
            this.type = type;
            this.children = new List<BT>(_children);
        }

        public BT(ActionFuntion _actionFunction)
        {
            this.type = NODE_TYPE.ACTION;
            this.actionFunction = _actionFunction;
        }


        public BT_VALUE Evaluate()
        {
            BT_VALUE value = BT_VALUE.FAIL;

            switch (this.type)
            {
                case NODE_TYPE.ACTION:
                    value = actionFunction();
                    break;
                case NODE_TYPE.SELECTOR:

                    break;
                case NODE_TYPE.SEQUENCE:
                    foreach (BT child in children)
                    {
                        value = child.Evaluate();
                        if (value != BT_VALUE.SUCCESS)
                        {
                            break;
                        }
                    }

                    break;
                case NODE_TYPE.INVERTER:
                    break;
                default:
                    break;
            }
            return value;
        }


    }





}
