## Sigma SciDefRenamer ##

# Forum Thread: http://forum.kerbalspaceprogram.com/index.php?/topic/160151-0/



## Available Functions


# Copy

Copies all science definitions for a planet (SOURCE) and assigns the copies to another planet (NEW).

---

# Delete

Deletes all science definitions for a planet (NAME).

---

# Rename

Renames all science definitions for a planet (OLD) so that they are assigned to another planet (NEW).

---

# Replace

Replaces all instances of a string (FIND) with another string (REPLACE).
This function can be limited to a certain planet by defining an additional parameter (PLANET).

---

# Swap

Swaps all science definitions between (THIS) planet and (THAT) planet.

---



## Syntax

You can fit as many functions as you want in a single SciDefRenamer node.
Functions will be executed in order.

//  START CODE  //

SciDefRenamer
{
	Copy
	{
		SOURCE = planet_to_be_copied
		NEW = new_planet_name
	}
	Delete
	{
		NAME = planet_name
	}
	Rename
	{
		OLD = old_planet_name
		NEW = new_planet_name
	}
	Rename
	{
		FIND = string_to_replace
		REPLACE = replacement_string
		
		PLANET = planet_name // This is Optional //
	}
	Swap
	{
		THIS = planet_name
		THAT = planet_name
	}
}

//   END CODE   //


## For other questions, visit the Forum Thread:
# http://forum.kerbalspaceprogram.com/index.php?/topic/160151-0/



## Sigma88 ##
