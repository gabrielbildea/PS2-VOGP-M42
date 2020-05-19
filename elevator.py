import tkinter as tk
HEIGHT=512
WIDTH=512

Var=3

root=tk.Tk()

canvas = tk.Canvas(root, height=HEIGHT, width=WIDTH)
canvas.pack()

background_label = tk.Label(root)
background_label.place(relwidth=1, relheight=1, )
background_image = tk.PhotoImage(file='elevator.png')
background_label = tk.Label(root, image=background_image)
background_label.place(relwidth=1, relheight=1 )


frame=tk.Frame(root, bg='green', bd=5)
frame.place(relx=0.5, rely=0.2, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='green', bd=5)
frame.place(relx=0.5, rely=0.35, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='green', bd=5)
frame.place(relx=0.5, rely=0.50, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='green', bd=5)
frame.place(relx=0.5, rely=0.65, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='green', bd=5)
frame.place(relx=0.5, rely=0.80, relwidth=0.75, relheight=0.1, anchor='n')

if Var == 4:
    frame = tk.Frame(root, bg='red', bd=5)
    frame.place(relx=0.5, rely=0.2, relwidth=0.75, relheight=0.1, anchor='n')
elif Var == 3:
    frame = tk.Frame(root, bg='red', bd=5)
    frame.place(relx=0.5, rely=0.35, relwidth=0.75, relheight=0.1, anchor='n')
elif Var == 2:
    frame = tk.Frame(root, bg='red', bd=5)
    frame.place(relx=0.5, rely=0.50, relwidth=0.75, relheight=0.1, anchor='n')
elif Var == 1:
    frame = tk.Frame(root, bg='red', bd=5)
    frame.place(relx=0.5, rely=0.65, relwidth=0.75, relheight=0.1, anchor='n')
elif Var == 0:
    frame = tk.Frame(root, bg='red', bd=5)
    frame.place(relx=0.5, rely=0.80, relwidth=0.75, relheight=0.1, anchor='n')
root.mainloop()
