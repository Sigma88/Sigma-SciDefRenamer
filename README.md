# Sigma SciDefRenamer

**Makes it easier to edit science definitions.**


KSP Forum Thread: http://forum.kerbalspaceprogram.com/index.php?/topic/160151-0/

Download Latest Release: https://github.com/Sigma88/Sigma-SciDefRenamer/releases/latest

Dev version: https://github.com/Sigma88/Sigma-SciDefRenamer/tree/Development

# Settings

<pre>
SigmaSciDefRenamer
{
}
</pre>

This is the root node, you can add as many as you want, and they can be modified using
[ModuleManager](http://forum.kerbalspaceprogram.com/index.php?/topic/50533-0/) patches.

The root node contains the following nodes:

  - **Rename**
    
    <pre>
    SigmaSciDefRenamer
    {
        Rename
        {
            <b>OLD</b> = <i>old_planet_name</i>
            <b>NEW</b> = <i>new_planet_name</i>
        }
    }
    </pre>
    
    Changes all science defs from planet **```OLD```** to planet **```NEW```**.

  - **Swap**
    
    <pre>
    SigmaSciDefRenamer
    {
        Swap
        {
            <b>THIS</b> = <i>planet_name</i>
            <b>THAT</b> = <i>planet_name</i>
        }
    }
    </pre>
    
    Swaps the science definitions of planets **```THIS```** and **```THAT```**.

  - **Copy**
    
    <pre>
    SigmaSciDefRenamer
    {
        Rename
        {
            <b>SOURCE</b> = <i>old_planet_name</i>
            <b>NEW</b> = <i>new_planet_name</i>
        }
    }
    </pre>
    
    Creates a new set of science definitions, copied from planet **```SOURCE```**, and assigns them to planet **```NEW```**.

  - **Delete**
    
    <pre>
    SigmaSciDefRenamer
    {
        Delete
        {
            <b>NAME</b> = <i>planet_name</i>
        }
    }
    </pre>
    
    Deletes all science definitions of planet **```NAME```**.

  - **Replace**
    
    <pre>
    SigmaSciDefRenamer
    {
        Replace
        {
            <b>FIND</b> = <i>string_to_be_replaced</i>
            <b>REPLACE</b> = <i>replacement_string</i>
            <b>PLANET</b> = <i>planet_name</i>
        }
    }
    </pre>
    
    Finds and replaces string **```FIND```** with string **```REPLACE```** in the key.

    String **```PLANET```** is optional, if omitted the changes will apply to all planets.

  - **Text**
    
    <pre>
    SigmaSciDefRenamer
    {
        Text
        {
            <b>FIND</b> = <i>string_to_be_replaced</i>
            <b>REPLACE</b> = <i>replacement_string</i>
            <b>PLANET</b> = <i>planet_name</i>
        }
    }
    </pre>
    
    Finds and replaces string **```FIND```** with string **```REPLACE```** in the science report text.

    String **```PLANET```** is optional, if omitted the changes will apply to all planets.
