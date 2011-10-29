
-- Variable Definitions

global currentFile                              -- name of currently open file

global numNodes


fn getFileName val =
( 
fname = filterString val ": \\"
fname = fname[fname.count]

return fname
)

rollout main "Level Exporter"
(
        label comments "Comments:"
        editText noteList text:"Comments:" height:100
        button writeFile "Export Level" across:1

        on writeFile pressed do
        (
                currentIn       = getSaveFileName caption:"Export File" types:"XML(*.xml)|*.xml|All|*.*|"
                if currentIn != undefined do
                (
                        currentFile = openFile currentIn mode: "wt"

                        -- Comments --
                        format "<!-- " to:currentFile
                        for a = 1 to Main.noteList.text.count do
                        (
                                if Main.noteList.text[a] == "\n" then format "\n" to:currentFile
                                else format "%" Main.noteList.text[a] to:currentFile
                        )
                        format " -->\n" to:currentFile


                       
                        format "<SCENE>\n" to:currentFile

                        for numNodes = 1 to objects.count do
                        (
						
						
								select objects[numNodes]
								
								--Find if any instance is there for this object
								InstanceMgr.GetInstances $ &instances
								
								--For each instance, make it unique
								if instances.count > 0 do
								   InstanceMgr.MakeObjectsUnique &instances #individual
								
								deselect $
								
											
                                if( objects[numNodes] == undefined) do continue

                                body = objects[numNodes] -- collision body to export
                                shape = classOf body

                                if(shape == dummy) then
                                (
                                        if( getUserProp body "Particle" != undefined)do
                                        (

                                        format "\t<Particle>" to: currentFile
                                        --format getUserProp body "Particle" to:currentFile
                                        format "\t\t\t<position value =\"%\" />\n" body.position  to:currentFile
                                        format "\t</Particle>" to:currentFile
                                        )

                                        if(getUserProp body "Constraint" != undefined) then
                                        (
                                                type = "Point_Point"
												format "\t<Constraint name=\"%\">\n" body.name to:currentFile	
												format "\t\t<type value=\"%\" />\n" type to:currentFile
                                                --local coord of distance from a to b
												format "\t\t<position>\n" to:currentFile
												format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
												format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
												format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile								
												format "\t\t</position>\n" to:currentFile								
												
												
                                                if(getUserProp body "BodyA" != undefined) do
                                                (
												bodya = getUserProp body "BodyA"
                                                       format "\t\t<child name=\"%\" />\n" bodya to:currentFile
                                                )
                                                if(getUserProp body "BodyB" != undefined) do
                                                (
												bodyb = getUserProp body "BodyB"
                                                     format "\t\t<parent name=\"%\" />\n" bodyb to:currentFile							 
                                                )
												format "\t\t<isBreakable value=\"%\" />\n" body.enabled to:currentFile
												format "\t</Constraint>\n" to:currentFile
                           
                                        )
										else
										(
										
										format "\t<dummy name=\"%\">\n" body.name to:currentFile
										format "\t\t<position>\n" to:currentFile									
										format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
										format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
										format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile								
										format "\t\t</position>\n" to:currentFile
										format "\t</dummy>\n" to:currentFile										
										)
                                )
								else if(shape == Omnilight) then
								(
								rgb = body.rgb
								multiplier = body.multiplier
								decay = body.farAttenEnd
								
								format "\t<pointlight name=\"%\">\n" body.name to:currentFile
								format "\t\t<position>\n" to:currentFile									
								format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
								format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
								format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile								
								format "\t\t</position>\n" to:currentFile
								format "\t\t<color>\n" to:currentFile
								format "\t\t\t<x> % </x>\n" rgb.red  to:currentFile
								format "\t\t\t<y> % </y>\n" rgb.green  to:currentFile
								format "\t\t\t<z> % </z>\n" rgb.blue  to:currentFile								
								format "\t\t</color>\n" to:currentFile
								format "\t\t<multiplier amount=\"%\" />\n" multiplier to:currentFile
								format "\t\t<decay value =\"%\" />\n" decay to:currentFile				
								format "\t</pointlight>\n" to:currentFile						
								
								)
								else if(shape == TargetCamera) then
								(
								format "\t\t<camera name=\"%\">\n" body.name to:currentFile
								format "\t\t<position>\n" to:currentFile
								format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
								format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
								format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile	
								format "\t\t</position>\n" to:currentFile
								format "\t\t</camera>\n" to:currentFile
									
								)
								else if(shape == Targetobject) then
								(
								format "\t\t<target name=\"%\">\n" body.name to:currentFile
								format "\t\t<position>\n" to:currentFile
								format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
								format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
								format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile	
								format "\t\t</position>\n" to:currentFile
								format "\t\t</target>\n" to:currentFile
									
								)
								else if(shape == Spray) then
								(
								format "\t<particle name=\"%\">\n" body.name to:currentFile
								--format getUserProp body "Particle" to:currentFile
								format "\t\t<position>\n" to:currentFile
									format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
									format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
									format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile	
								format "\t\t</position>\n" to:currentFile
								tt = "Spray"
								format "\t\t\t<type value =\"%\" />\n" tt  to:currentFile
								format "\t\t\t<width value =\"%\" />\n" body.emitterwidth  to:currentFile
								format "\t\t\t<height value =\"%\" />\n" body.emitterheight  to:currentFile
								
								format "\t</particle>" to:currentFile
								
								)
								else if(shape == SuperSpray) then
								(
								format "\t<particle name=\"%\">\n" body.name to:currentFile
								--format getUserProp body "Particle" to:currentFile
								format "\t\t<position>\n" to:currentFile
									format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
									format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
									format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile	
								format "\t\t</position>\n" to:currentFile
								
								format "\t\t<rotation>\n" to:currentFile
									format "\t\t\t<w> % </w>\n" body.rotation.w  to:currentFile
									format "\t\t\t<x> % </x>\n" -body.rotation.x  to:currentFile
									format "\t\t\t<y> % </y>\n" body.rotation.y  to:currentFile	
									format "\t\t\t<z> % </z>\n" body.rotation.z  to:currentFile	
								format "\t\t</rotation>\n" to:currentFile
								
								tt = "DirectionalSpray"
								format "\t\t\t<type value =\"%\" />\n" tt  to:currentFile
								format "\t</particle>" to:currentFile
								)
								
								else if(shape == targetSpot) then
								(
								rgb = body.rgb
								multiplier = body.multiplier
								decay = body.decayRadius
								
								
								
								format "\t\t<spotlight name=\"%\">\n" body.name to:currentFile
								format "\t\t<enabled>%</enabled>\n" body.enabled  to:currentFile
								format "\t\t<fallof>%</fallof>\n" body.falloff  to:currentFile
								format "\t\t<castShadows>%</castShadows>\n" body.castShadows  to:currentFile
								format "\t\t<position>\n" to:currentFile
								format "\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
								format "\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
								format "\t\t\t<z> % </z>\n" body.position[2]  to:currentFile								
								format "\t\t</position>\n" to:currentFile
								
								
								format "\t\t<color>\n" to:currentFile
								format "\t\t\t<x> % </x>\n" rgb.red  to:currentFile
								format "\t\t\t<y> % </y>\n" rgb.green  to:currentFile
								format "\t\t\t<z> % </z>\n" rgb.blue  to:currentFile								
								format "\t\t</color>\n" to:currentFile
								
								
								format "\t\t\t<multiplier amount=\"%\" />\n" multiplier to:currentFile
								format "\t\t\t<decay value =\"%\" />\n" decay to:currentFile				
								format "\t</spotlight>\n" to:currentFile			
								
								
								
								)
								else if(shape == Point_Point) then
								(
								
									dpos = body.parentBody.position
									cpos = body.childBody.position
									npos = (dpos + cpos)/2  
									
									type = "Point_Point"
									format "\t<Constraint name=\"%\">\n" body.name to:currentFile	
									format "\t\t<type value=\"%\" />\n" type to:currentFile
									format "\t\t<position>\n" to:currentFile
									format "\t\t\t<x> % </x>\n" npos[1]  to:currentFile
									format "\t\t\t<y> % </y>\n" npos[3]  to:currentFile
									format "\t\t\t<z> % </z>\n" npos[2]  to:currentFile								
									format "\t\t</position>\n" to:currentFile								
									format "\t\t<child name=\"%\" />\n" body.childBody.name to:currentFile
									
									--replacing the position
									body.position = dpos
									
									if( body.hasParent ==  true) do
									(
									
									format "\t\t<parent name=\"%\" />\n" body.parentBody.name to:currentFile							
									
									)
									format "\t\t<isBreakable value=\"%\" />\n" body.enabled to:currentFile
									
																	
									format "\t</Constraint>\n" to:currentFile
								)
                                else if ( shape != RBCollection and shape!= CSolver) then
                                (


                                format "i:% body: % \n\n " l body
                                format "\t<body " to:currentFile

                                -- Bone and CLass and Name --
                        
                                
                                format "name=\"%\" >\n" body.name to:currentFile

								if(getUserProp body "PhysicType" != undefined   ) then
								(
									fis = getUserProp body "PhysicType"
									
									if(fis == "Water")then
									(
									format "\t\t<collision type=\"%\">\n" fis to:currentFile
									format "\t\t\t<position>\n" to:currentFile									
									format "\t\t\t\t<x> % </x>\n" body.position[1]  to:currentFile
									format "\t\t\t\t<y> % </y>\n" -body.position[3]  to:currentFile
									format "\t\t\t\t<z> % </z>\n" body.position[2]  to:currentFile								
									format "\t\t\t</position>\n" to:currentFile
									format "\t\t\t<width value=\"%\"/>\n" body.width to:currentFile
									format "\t\t\t<length value=\"%\"/>\n" body.length to:currentFile									
									format "\t\t</collision>\n" to:currentFile
									)
									else
									(
									format "\t\t<collision type=\"%\"/>\n" fis to:currentFile
									)
								)
								else
								(
								fis = "TriangleMesh"
								format "\t\t<collision type=\"%\"/>\n" fis to:currentFile
								
								)
								
								
								
								if(getUserProp body "Mass" != undefined) do
                                (					
								mass = getUserProp body "Mass"
							    format "\t\t<mass value=\"%\" />\n" mass to:currentFile
								)
								
								
								if(getUserProp body "Ellasticity" != undefined) do
                                (				
								
								
								elas = getUserProp body "Ellasticity"
								format "\t\t<ellasticity value=\"%\" />\n" elas to:currentFile								
								)
								
								
								
								if(getUserProp body "DinamicFriction" != undefined) do
                                (			
								flag = true								
								fric = getUserProp body "DinamicFriction"
								format "\t\t<dinamicfriction value=\"%\" />\n" fric to:currentFile								
								)
								if(getUserProp body "StaticFriction" != undefined) do
                                (								
								flag = true								
								fric = getUserProp body "StaticFriction"
								format "\t\t<staticfriction value=\"%\" />\n" fric to:currentFile								
								)
								
								if(classOf body.material == RaytraceMaterial)do
								(
								mat = classOf body.material
								format "\t\t<material name=\"%\">\n" mat to:currentFile
								if(body.material.environmentMap != undefined) do
									(
									a = getFileName body.material.environmentMap.filename
									format "\t\t\t<environment name =\"%\" />\n" a  to:currentFile
									)
								
									
								format "\t\t</material>\n" to:currentFile
								
								)
								
								
								
								if(classOf body.material ==  Standardmaterial) do
								(
								
									mat = classOf body.material
								format "\t\t<material name=\"%\">\n" mat to:currentFile
									
																	
									format "\t\t\t<SpecularIntensity>\n" to:currentFile
									format "\t\t\t%\n" body.material.Glossiness  to:currentFile
									format "\t\t\t</SpecularIntensity>\n" to:currentFile
									
									format "\t\t\t<SpecularPower>\n" to:currentFile
									format "\t\t\t%\n" body.material.specularLevel  to:currentFile
									format "\t\t\t</SpecularPower>\n" to:currentFile
									
									if(body.material.reflectionmap != undefined) then
									(
									a = getFileName body.material.reflectionmap.filename
										format "\t\t\t<reflection name =\"%\" />\n" a  to:currentFile
									
									)									
									else
									(
										if(body.material.diffusemap != undefined) do
										(
										a = getFileName body.material.diffusemap.filename
										format "\t\t\t<diffuse name =\"%\" />\n" a  to:currentFile
										)
										if(body.material.bumpmap != undefined) do
										(
										a =getFileName body.material.bumpmap.filename
										format "\t\t\t<bump name =\"%\" />\n" a to:currentFile
										)
										if(body.material.specularlevelmap != undefined) do
										(
										a = getFileName body.material.specularlevelmap.filename
										format "\t\t\t<specular name =\"%\" />\n" a to:currentFile
										)
										if(body.material.selfillummap != undefined) do
										(
										a = getFileName body.material.selfillummap.filename
										format "\t\t\t<glow name =\"%\" />\n" a to:currentFile													
										)
									)
									format "\t\t</material>\n" to:currentFile
							
								)
								
                       

                                format "\t</body> \n"to:currentFile
                                )
                        )

                        format "</SCENE>\n" to:currentFile
                        close currentFile
                )
        )
)-- create the rollout window and add the rollout

if MoCapFloater != undefined do
(
        closerolloutfloater MoCapFloater
)

MoCapFloater = newRolloutFloater "PloobsLevel Exporter" 500 200 100 100

addRollout Main MoCapFloater