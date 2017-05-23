﻿using UnityEngine.Graphing;
using System.Linq;
using System.Collections;

namespace UnityEngine.MaterialGraph
{
    [Title("Math/Advanced/AddMultiple")]
    public class AddManyNode : FunctionNInNOut, IGeneratesFunction
    {
        int m_nodeInputCount = 2;

        public void AddInputSlot()
        {
            string inputName = "Input" + GetInputSlots<ISlot>().Count().ToString();
            AddSlot(inputName, inputName, Graphing.SlotType.Input, SlotValueType.Dynamic);
        }

        public AddManyNode()
        {
            name = "AddMany";
            for(int i = 0; i < m_nodeInputCount; ++i)
            {
                AddInputSlot();
            }

            AddSlot("Sum", "finalSum", Graphing.SlotType.Output, SlotValueType.Dynamic);
            UpdateNodeAfterDeserialization();
        }

        public void OnModified()
        {
            if (onModified != null)
                onModified(this, ModificationScope.Node);
        }

        protected override string GetFunctionName()
        {
            return "unity_AddMultiple";
        }

        string GetSumOfAllInputs()
        {
            string sumString = "";
            int inputsLeft = GetInputSlots<ISlot>().Count();

            foreach (ISlot slot in GetInputSlots<ISlot>())
            {
                sumString += GetShaderOutputName(slot.id);
                if (inputsLeft > 1)
                    sumString += " + ";
                --inputsLeft;
            }

            return sumString;
        }

        public void GenerateNodeFunction(ShaderGenerator visitor, GenerationMode generationMode)
        {
            var outputString = new ShaderGenerator();
            outputString.AddShaderChunk(GetFunctionPrototype(), false);
            outputString.AddShaderChunk("{", false);
            outputString.Indent();
            outputString.AddShaderChunk("finalSum = " + GetSumOfAllInputs() + ";", false);
            outputString.Deindent();
            outputString.AddShaderChunk("}", false);

            visitor.AddShaderChunk(outputString.GetShaderString(0), true);
        }
    }
}
