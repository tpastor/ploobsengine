

phistype = "Box"

rollout MainRollout "Basic Characteristic" width:184 height:358
(
	dropdownList ptype "Physics Type" pos:[8,16] width:168 height:40 items:#("Box", "Sphere", "TriangleMesh")

	button btn1 "OK" pos:[37,289] width:104 height:32
	dropdownList mtype "Material" pos:[11,98] width:165 height:40 enabled:true items:#("Wood", "Metal", "Rubber") selection:2
	label lbl2 "dinamic friction" pos:[14,189] width:76 height:24
	spinner spn2 "" pos:[98,189] width:66 height:16 range:[0,100,0]
	label lbl3 "static friction" pos:[13,218] width:72 height:24
	spinner spn3 "" pos:[97,218] width:66 height:16 range:[0,100,0]
	label lbl7 "Mass" pos:[14,66] width:58 height:24
	spinner spn7 "" pos:[99,68] width:66 height:16 range:[0,100,0]
	label lbl24 "ellasticity" pos:[13,164] width:58 height:24
	spinner spn24 "" pos:[97,164] width:66 height:16 range:[0,100,0]
	button AddConstraint "Add Constraints" pos:[45,256] width:90 height:21 
	
	on btn1 pressed do
	(
		
		if(ptype.selection == 1) do
		(
			phistype = "Box"
		)
		if(ptype.selection == 2) do
		(
			phistype = "Sphere"
		)
		if(ptype.selection == 3) do
		(
			phistype = "TriangleMesh"
		)
		
		for o in selection do
		(	
			setUserProp o "DinamicFriction" spn2.value
			setUserProp o "Ellasticity" spn24.value		
			setuserProp o "Mass" spn7.value			
			setUserProp o "PhysicType" phistype			
			setUserProp o "StaticFriction" spn3.value
				
		)
	
	)
	on mtype selected sel do
	(
		
		
		-- wood properties
			if(mtype.selection == 1) do 
			(		
				spn24.value = 1
				spn2.value = 1
				spn3.value = 1		
			)
			
			-- metal properties
			if(mtype.selection == 2) do 
			(		
				spn24.value = 1
				spn2.value = 1
				spn3.value = 1		
			)
			-- rubber properties
			if(mtype.selection == 3) do 
			(		
				spn24.value = 1
				spn2.value = 1
				spn3.value = 1		
			)
		
	)
	on AddConstraint pressed do
	(
		a = selection[1]
		 for b in selection do
		 (
		   if (a == b) do continue
		   d = dummy();
		   d.pos = (a.pos + b.pos)/2  
		   setUserProp d "Constraint" "Point2Point"
		   setUserProp d "BodyA" a.name
		   setUserProp d "BodyB" b.name
		   scale d [0.1,0.1,0.1]
		   a = b
		 )
	)
)
CreateDialog MainRollout