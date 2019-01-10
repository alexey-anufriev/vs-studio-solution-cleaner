# Problem
Visual Studio does not clean up 'obj' and 'bin' folders even during 'Clean' operation.

# Solution
Small extension 'SolutionCleaner' extends context menu for Project/Solution with a new operation that can do the clean-up.

# How To Use?
Just install the extension linked in "Where To Get?" section and run it for project or solution.

## For Project
Just a single project is cleaned up.

1) Right-click on a project.
2) Pick "Clean Up Project":

![1.png](https://i.ibb.co/60h5qcG/1.png)

3) Popup with clean up results will appear:

![2.png](https://i.ibb.co/tpxZ6cw/2.png)

##For Solution
All the child projects will be cleaned up.

1) Right-click on a solution.
2) Pick "Clean Up Solution"

![3.png](https://i.ibb.co/RS1MS6t/3.png)

3) Popup with clean up results will appear:

![4.png](https://i.ibb.co/n71Xbtt/4.png)
