   M S B A M L   `   `   `  ����X  TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit  5   	   B >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject 5   	      Project.DrawPolygonWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Ocontrols:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     5   	   'localclr-namespace:Project     5   	     � Title$  Draw Polygon��5	   	   $	��300��6   $	��440��6+   
�������.��5      �  � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��* �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Stroke Thickness:��6   $��1��6,   $��Center=�5      $��Center=�6+   �� 5   
   -   5         ��Name$ strokeThickness��5      $��1��6+   $��1��6;   $��Center=�5       $��Stretch=�6-   $��5 0 0 0q�5!      $��Center=�6$   $��18(�6F   $��35��5"      $	��210��6    �� TextWrapping$
 Wrap=�5#      �� 5&   
   $��Stroke:��6   $��2��6"   $��Center=�5'      $��Center=�6+   #   Xceed.Wpf.Toolkit.ColorPicker  5(   
   -   5          Name$ strokeColor��5,      $��2��5(      $��Center=�5)      $��Center=�6:   $��35��5*      $	��210��6+   $��1��5+      $��2��5,   4   �� 5/   
   $��Fill:��6   $��3��6    $��Center=�50      $��Center=�6+     51   
   -   5        $ 	fillColor��55      $��3��51      $��Center=�52      $��Center=�6:   $��35��53      $	��210��6+   $��1��54      $��2��55   2   �� 58   
   $��4��6   $��0,1,0,0q�6   
���   �$��2��6L   �� 5;   
   $��Text:��6   $��4��6    $��Center=�5<      $��Center=�6+   �� 5=   
   -   5        $ textBox��5=      $��1��6#   $��4��63   $��Center=�5>      $��Stretch=�6-   $��5 0 0 0q�5?      $��Center=�6$   $��18(�6F   $��35��5@      $	��210��6   $
 Wrap=�5A      �� 5D   
   $��Color:��6   $��5��6!   $��Center=�5E      $��Center=�6+     5F   
   -   5        $ 	textColor��5J      $��5��5F      $��Center=�5G      $��Center=�6:   $��35��5H      $	��210��6+   $��1��5I      $��2��5J   2   �� 5M   
   $��	Is Clear:��6   $��6��6$   $��Center=�5N      $��Center=�6+   �� 5O   
   -   5         ��Name$ polygonIsTransparent��5P      $��1��5O      $��6��6#   $��Center=�5Q      $��Center=�6.   $��0,0,25,0q�5R      � 5U   
   $��2��6   $��8��6#    5V      �� 5W      $��* �6#   �� 5X      $��* �6#   .+�5\      �� -   5         ��Name$ drawPolygonButton��5`      $
��Draw��5\      $��0��5]      
�������5`      �� 5c      -   5        $��Close��5c      $��1��5d      
�������5f      5g      5h      5i      