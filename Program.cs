using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

public class DailyCheckApp : Form
{
    private CheckBox[] taskCheckboxes;
    private string saveFile = "tasks.txt";

    public DailyCheckApp()
    {
        Text = "Daily Check";
        Width = 300;
        Height = 250;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        string[] tasks = { "Exercise", "Read a book", "Drink water", "Meditate", "Plan tomorrow" };
        taskCheckboxes = new CheckBox[tasks.Length];

        for (int i = 0; i < tasks.Length; i++)
        {
            taskCheckboxes[i] = new CheckBox { Text = tasks[i], Left = 20, Top = 30 + (i * 30), Width = 250 };
            Controls.Add(taskCheckboxes[i]);
        }

        Button saveButton = new Button { Text = "Save", Left = 100, Top = 180, Width = 80 };
        saveButton.Click += SaveTasks;
        Controls.Add(saveButton);

        LoadTasks();
    }

    private void SaveTasks(object? sender, EventArgs? e)
    {
        using (StreamWriter writer = new StreamWriter(saveFile))
        {
            foreach (var checkbox in taskCheckboxes)
            {
                writer.WriteLine(checkbox.Checked);
            }
        }
        MessageBox.Show("Tasks saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void LoadTasks()
    {
        if (File.Exists(saveFile))
        {
            string[] lines = File.ReadAllLines(saveFile);
            for (int i = 0; i < lines.Length && i < taskCheckboxes.Length; i++)
            {
                taskCheckboxes[i].Checked = bool.Parse(lines[i]);
            }
        }
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DailyCheckApp());
    }
}
