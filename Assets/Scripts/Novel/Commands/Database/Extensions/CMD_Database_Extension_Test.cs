using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMMANDS
{
    public class CMD_Database_Extension_Test : CMD_DatabaseExtension
    {
        new public static void Extend(CommandDatabase database)
        {
            //Add command with no params

            database.AddCommand("print", new Action(PrintDefaultMessage));
        }

        private static void PrintDefaultMessage()
        {
            Debug.Log("Printing a default message to console.");
        }
    }
}