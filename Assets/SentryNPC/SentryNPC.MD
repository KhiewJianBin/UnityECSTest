
This document describes SentryNPC. A System that gives NPC the behaviour of natural behaviour and goals depending on certain factors. Similar to Radiant AI Bethesda


Level 0 - General Dialog Commentary On World Status & General Status
- NPC will comment on Scripted Events
	- "Did you see the murder in the town? How horrible!"
- NPC will comment on it's own Wellbeing, Status & Stats
	- "I have not eaten all day" (hunger)
	- "I am getting tried" (fatigue)
- NPC Will comment on Big World Changes
	- "The king has been killed"
	- "The hero has killed the demon king"
  
Level 1 - Dialog Commentary Reaction to Player Acted Events
- NPC will comment on Player's Equipment
	- "Nice Sword you got there"
- NPC will comment on Player's Wellbeing, Status & Stats
	- "You look tired, you should get some rest"
- NPC will comment on Player's Obtaining In-game Achivement
	-	"I heard you have become the new hero"
- NPC will comment on Player's Quest Progression
	- "So you are the one who will save the princess. Well good luck"
- NPC will comment and give hints on hidden Bossess/Items/Quest
	- "I heard if there's a strong beast in a cave near south of town" 
  
Level 2 - NPC respond on Player's Action
- NPC will be defensive against Aggresive Action (Sneaking,Carrying Weapon)
- NPC will react based on Character Alighment (Law vs Chaos, Good vs Evil) - Similar to Fable, where NPC will react and run away if player has negative karma and will run towards player if positive karma
- 
Level 3 - NPC have set Move Schedules and act accordingly
- NPC will have precoded schedules and will move from places to places. (Home->Shopping->Visit->Bar->Sleep)
- 
Level 4 - NPC have Stats & Statuses and will modify Level 3
- NPC will move faster or slower based on Stats
- NPC can get Status like "Hurt" which will augment Stats
- NPC can get Status like "Sick" which will change Move Schedules

Level 5a - NPC can talk to each other. Gather and give information
- NPC will record events seen by them up to max of a few days
- NPC will invoke conversation to transfer of information to Player or other NPC
	- "John has picked up gold recently"
- 
- Level 5b - NPC have goals which they will try to fulfil with the given information.
- NPC that have Status like "Hungry" will seek out food given information they have and allowable actions.
- NPC will seek out information if no information is available

Level 6 - NPC have behaviour charateristic such as "Lazy" which slightly augments Level 1-5
- NPC that are "Greedy" will buy things for less, and sell things for more.
- NPC that are "murderous" will resort to killing to get what they want.