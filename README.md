# AI-Companion

This is a project I created in 2016 for a university project for the purposes of using AI behaviour trees. 
If you check under Assets/Scripts/AI/Behaviour Tree, you can find all the scripts relating to how to create the tree. 
AIBrain is the root of the tree, and contains the blackboard for the tree.

Blackboard is essentially a 'catch-all' dictionary for any type of data the AI needs to store. It isn't type specific, so make sure your string is spelt correcttly.

Under Nodes you can find the plethora of nodes to create the trees. These all come back to three main types:
  Composite
  Decorator
  Action

You can find much more detail here on what these mean:

https://www.gamasutra.com/blogs/ChrisSimpson/20140717/221339/Behavior_trees_for_AI_How_they_work.php

This was a good resource for me when creating the project.

If you click "Display Grid" which can be found in the inspector, under Grid Script, for the GameController. When you hit play, you can look in the scene window to see the pathfinding grid be generated, as well as AI path finding, including the companion AI determining its position behind the player- which is based on Ellie's AI from The Last of Us. 

If you look at one of the simple enemy type scrips in Assets/Scripts/AI/EnemyGuard.cs you can see how this all comes together into a single tree.

I've tried to format it as clear as possible by using indents to indicate which nodes are children of other nodes.
But essentially all this tree does is:

NodeSequencer root = new NodeSequencer(new BTNode[]        --- Create a Sequencer root

            {
            
             new NodeSelector(new BTNode[]              --- Use a selector node (See above link).
             
             {
             
                    new ActionCheckForEnemiesInSight(),    --- Are there any enemies in my line of sight? If there are, we won't go to the next node, and instead go to the next in sequence- which is ActionUseWeapon. 
                    
                    new NodeAlwaysFail(                    --- This node always fails so the tree will constantly tick ActionLookForEnemy and then restart the sequence, using ActionCheckForEnemiesInSight until ActionCheckForEnemiesInSight returns a sucess and attacks.
                    
                      new ActionLookForEnemy()
                      
                    )
                    
                }),
                
                new ActionUseWeapon()
                
           });

You can load in a prefab to see how all the scripts work together. As well as looking at Main.scene in Unity.

Entity.cs is the parent class which all AI types inherits from. This contains all functions that are common to all of them. Such as health, speed, requesting a path etc.

If you want even more information, including a YouTube video, and you can read the report I wrote on the project which details mostly everything, including diagrams of the full trees for each AI type here: 

http://elliottayling.co.uk/portfolio/first-person-companion-game/

There is also an implementation of dynamically generated A* pathfinding with heap optimisation under Assets/Scripts/AI/Pathfinding.
Which is based on this video: https://www.youtube.com/watch?v=3Dw5d7PlcTM

Texture and sound sources:

http://opengameart.org/content/decorative-floor-free-tiling-texture

http://opengameart.org/content/glowing-hexagon-free-tiling-texture

http://opengameart.org/content/plasmatic-metal-2-free-tiling-textures

https://www.assetstore.unity3d.com/en/#!/content/25117

http://opengameart.org/content/gunloop-8bit

http://opengameart.org/content/explosion-0

http://opengameart.org/content/2-high-quality-explosions
